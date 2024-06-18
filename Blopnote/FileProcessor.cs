using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blopnote
{
    internal class FileProcessor
    {
        private readonly TextField textField;
        private readonly FileState fileState;
        private readonly LyricsBox lyricsBox;
        private readonly OpenFileDialog openFileDialog;

        internal event EventHandler DirectoryChanged;

        internal int DirectoryLength => fileState.directoryInfo.FullName.Length;

        private string LyricsPath => MakeLyricsPath(fileState.FilePath);

        private string MakeLyricsPath(string filePath)
        {
#warning dirty and obfuscated
            int indexOfLastSlash = filePath.LastIndexOf('\\');
            string lyricsPath = filePath.Insert(indexOfLastSlash, "\\" + Names.SongInfoFolder)
                                        .Replace(".txt", ".json");
            return lyricsPath;
        }

        internal FileProcessor(TextField textField, FileState fileState, LyricsBox lyricsBox, OpenFileDialog openFileDialog)
        {
            this.textField = textField;
            this.fileState = fileState;
            this.lyricsBox = lyricsBox;
            this.openFileDialog = openFileDialog;
        }

        internal void SetInitialDirectory(string directoryName)
        {
            fileState.directoryInfo = new DirectoryInfo(directoryName);
            openFileDialog.InitialDirectory = directoryName;
            EnsureSongsInfoFolder();
            DirectoryChanged(this, null);
        }

        private void EnsureSongsInfoFolder()
        {
            var subDirectories = (from dir in fileState.NestedFolders
                                  where dir.Name.ToLower() == Names.SongInfoFolder
                                  select dir);
            try
            {
                EnsureNameInLowerCase(subDirectories.First());
            }
            catch (InvalidOperationException)
            {
                fileState.directoryInfo.CreateSubdirectory(Names.SongInfoFolder);
            }
        }

        private void EnsureNameInLowerCase(DirectoryInfo SongsInfoDirectory)
        {
            if (SongsInfoDirectory == null)
            {
                throw new ArgumentException("directory can't be null");
            }

            if (SongsInfoDirectory.Name != Names.SongInfoFolder)
            {
                SongsInfoDirectory.NameToLower();
            }
        }

        internal void CreateNewTranslation(FileInfo fileInfo, SongInfo songInfo)
        {
            fileState.NewFileInCurrentDir(fileInfo, songInfo);

            File.Create(fileState.FullFileName).Dispose();

            if (fileState.LyricsIsUsed)
            {
                var filteredLyrics = lyricsBox.FilterAndStore(songInfo.Lyrics);
                fileState.UpdateLyrics(filteredLyrics);
                WriteLyricsToJSON();

                textField.ObserveCompletion();
            }
            else
            {
                lyricsBox.EnsureCleared();
            }
        }

        internal void Save()
        {
            WriteInFile(fileState.FullFileName, textField.Text);
        }

        private void WriteLyricsToJSON()
        {
            string serializedSongInfo = JsonConvert.SerializeObject(fileState.songInfo);
            File.WriteAllText(LyricsPath, serializedSongInfo);
        }

        private void WriteInFile(string filePath, string text)
        {
            using (var writer = new StreamWriter(path: filePath,
                                                 append: false,
                                                 encoding: Encoding.UTF8))
            {
                writer.Write(text);
            }
        }

        internal void OpenTranslation(string fileName)
        {
            fileState.OpenFileWithDir(fileName);
            ParseSongInfo();
            SetInitialDirectory(fileState.directoryInfo.FullName);

            textField.Text = File.ReadAllText(fileName);
            if (fileState.LyricsIsUsed)
            {
                lyricsBox.FilterAndStore(fileState.songInfo.Lyrics);
                if (!fileState.songInfo.Completed)
                {
                    textField.ObserveCompletion();
                }
            }
        }

        private void ParseSongInfo()
        {
            string conjectiveSongInfoPath = MakeLyricsPath(fileState.FullFileName);
            if (File.Exists(conjectiveSongInfoPath))
            {
                fileState.songInfo = (SongInfo)JsonConvert.DeserializeObject
                (
                    value: File.ReadAllText(conjectiveSongInfoPath),
                    type: typeof(SongInfo)
                );
            }
            else
            {
                fileState.songInfo = null;
            }
        }

        internal void SongIsWritten_Handler(object sender, EventArgs e)
        {
            fileState.songInfo.Completed = true;
            try
            {
                File.WriteAllText(LyricsPath, JsonConvert.SerializeObject(fileState.songInfo));
            }
            catch (Exception exception)
            {
                MessageBox.Show(caption: "Error",
                            text: "Information about song completion wasn't written. Cause: " + exception.Message,
                            buttons: MessageBoxButtons.OK,
                            icon: MessageBoxIcon.Error
                            );
            }
            // release info since it's useless in the future
            fileState.songInfo = null;
            textField.StopObserving();
            MessageBox.Show(caption: "Completed",
                            text: "Congratulations! Song was successfully written!",
                            buttons: MessageBoxButtons.OK,
                            icon: MessageBoxIcon.Information
                            );
        }
    }
}