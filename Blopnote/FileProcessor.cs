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

        private DirectoryInfo directory;
        internal event EventHandler DirectoryChanged;

        internal int DirectoryLength => directory.FullName.Length;

        private string FilePath => Path.Combine(directory.FullName, fileState.FileName);
        private string LyricsPath => MakeLyricsPath(FilePath);

        private string MakeLyricsPath(string filePath)
        {
#warning dirty and obfuscated
            //string lyricsPath = filePath.Insert(filePath.Length - 4, " " + Names.SongInfoFolder);
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

        internal void ChangeDirectory(string directoryName)
        {
            directory = new DirectoryInfo(directoryName);
            EnsureSongsInfoFolder();
            openFileDialog.InitialDirectory = directoryName;
            DirectoryChanged(this, null);
        }

        private void EnsureSongsInfoFolder() 
        {
            var subDirectories = (from dir in directory.GetDirectories()
                                  where dir.Name.ToLower() == Names.SongInfoFolder
                                  select dir);
            try
            {
                EnsureNameInLowerCase(subDirectories.First());
            }
            catch (InvalidOperationException)
            {
                directory.CreateSubdirectory(Names.SongInfoFolder);
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

        internal void CreateNewTranslation(string fileName, string lyrics)
        {
            fileState.UpdateState(fileName, lyrics, directory);

            File.Create(FilePath).Dispose();

            if (fileState.LyricsIsUsed)
            {
                var filteredLyrics = lyricsBox.FilterAndKeep(lyrics);
                fileState.CreateSongInfo(filteredLyrics);
                WriteLyrics();
            }
            else
            {
                lyricsBox.EnsureCleared();
            }
        }

        internal void Save()
        {
            WriteInFile(FilePath, textField.Text);
        }

        private void WriteLyrics()
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

        internal void OpenTranslation(string FullFileName)
        {
#warning i'm so tired with this shitcode
            string lyrics = FindLyrics(FullFileName);
            string fileName = FullFileName.Substring(FullFileName.LastIndexOf('\\') + 1);
            string directory = FullFileName.Substring(0, FullFileName.LastIndexOf('\\') + 1);
            ChangeDirectory(directory);
            fileState.UpdateState(fileName, lyrics, this.directory);
            textField.Text = File.ReadAllText(FullFileName);
            if (fileState.LyricsIsUsed)
            {
                lyricsBox.FilterAndKeep(lyrics);
                if (!fileState.songInfo.Completed)
                {
                    textField.ObserveCompletion();
                }
            }
        }

        //private void ReadText(string FullFileName)
        //{
        //    using(var reader = new StreamReader(FullFileName, Encoding.UTF8))
        //    {
        //        textField.Text = reader.ReadToEnd();
        //    }
        //}

        private string FindLyrics(string FullFileName)
        {
            // C:\Users\azgel\Desktop\translations\ic3peak - no death.txt
            string conjectiveLyricsPath = MakeLyricsPath(FullFileName);
            if (File.Exists(conjectiveLyricsPath))
            {
                fileState.LyricsIsUsed = true;
                fileState.ReadSongInfo(conjectiveLyricsPath);
                return fileState.songInfo.Lyrics;
            }
            else
            {
                return string.Empty;
            }
        }

        internal void SongIsWritten_Handler(object sender, EventArgs e)
        {
            fileState.songInfo.Completed = true;
            File.WriteAllText(LyricsPath, JsonConvert.SerializeObject(fileState.songInfo));
            textField.StopObserving();
            MessageBox.Show(caption: "Completed",
                            text: "Congratulations! Song was successfully written!",
                            buttons: MessageBoxButtons.OK,
                            icon: MessageBoxIcon.Information
                            );
        }
    }
}