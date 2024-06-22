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

            if (fileState.IsLyricsUsed)
            {
                fileState.Lyrics = lyricsBox.FilterAndStore(fileState.Lyrics);
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
            File.WriteAllText(fileState.FullFileName, serializedSongInfo);
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
            fileState.songInfo = ParseSongInfo();

            SetInitialDirectory(fileState.directoryInfo.FullName);

            textField.Text = File.ReadAllText(fileName);
        }

        private SongInfo ParseSongInfo()
        {
            string conjectiveSongInfoPath = GenerateSongInfoPath(fileState.FullFileName);
            if (File.Exists(conjectiveSongInfoPath))
            {
                return (SongInfo)JsonConvert.DeserializeObject
                (
                    value: File.ReadAllText(conjectiveSongInfoPath),
                    type: typeof(SongInfo)
                );
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// C:\path\to\file.txt => C:\path\to\SongInfo\file.json
        /// </summary>
        /// <param name="fullFileName"></param>
        /// <returns></returns>
        private string GenerateSongInfoPath(string fullFileName) =>
            fullFileName.Insert(fullFileName.LastIndexOf('\\'), "\\" + Names.SongInfoFolder).Replace(".txt", ".json");

        internal void SongIsWritten_Handler(object sender, EventArgs e)
        {
            fileState.songInfo.Completed = true;
            TryRewriteSongInfo("Information about song completion wasn't written.");
            #region trash
            //// release info since it's useless in the future // UPD: me in the past, are you chump?
            //fileState.songInfo = null; 
            #endregion
            textField.StopObserving();
            MessageBox.Show(caption: "Completed",
                            text: "Congratulations! Song was successfully written!",
                            buttons: MessageBoxButtons.OK,
                            icon: MessageBoxIcon.Information
                            );
        }

        internal void TryRewriteSongInfo(string errorMessage)
        {
            try
            {
                File.WriteAllText(GenerateSongInfoPath(fileState.FullFileName), JsonConvert.SerializeObject(fileState.songInfo));
            }
            catch (Exception exception)
            {
                MessageBox.Show(caption: "Error",
                            text: errorMessage + " Cause of error: " + exception.Message,
                            buttons: MessageBoxButtons.OK,
                            icon: MessageBoxIcon.Error
                            );
                return;
            }
        }
    }
}