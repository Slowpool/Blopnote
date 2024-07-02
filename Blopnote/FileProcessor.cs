﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Blopnote
{
    public class FileProcessor
    {
        private readonly TextField textField;
        private readonly FileState fileState;
        private readonly LyricsBox lyricsBox;
        private readonly OpenFileDialog openFileDialog;
        private readonly SaveFileDialog saveFileDialog;
        private readonly FolderBrowserDialog folderBrowserDialog;
        private XmlSerializer xmlSerializer { get; set; }

        public event EventHandler DirectoryChanged;

        public FileProcessor(TextField textField, FileState fileState, LyricsBox lyricsBox, OpenFileDialog openFileDialog)
        {
            this.textField = textField;
            this.fileState = fileState;
            this.lyricsBox = lyricsBox;
            this.openFileDialog = openFileDialog;

            folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Choose the folder where translations will be stored. Programm will create the folder 'SongsInfo' within.";
            saveFileDialog = new SaveFileDialog();
        }

        public void SetInitialDirectory(string directoryName)
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

        public void CreateNewTranslation(FileInfo fileInfo, SongInfo songInfo)
        {
            fileState.NewFileInCurrentDir(fileInfo, songInfo);

            File.Create(fileState.FullFileName).Dispose();
#warning i don't like it
            if (fileState.IsLyricsUsed)
            {
                fileState.Lyrics = lyricsBox.FilterAndStore(fileState.Lyrics);
                textField.ObserveCompletion();
            }
            if (fileState.IsLyricsUsed || fileState.IsUrlUsed)
            {
                TryRewriteSongInfo("File with song information wasn't created.");
            }
        }

        public void Save()
        {
            File.WriteAllText(fileState.FullFileName, textField.Text, Encoding.UTF8);
        }

        private void WriteSongInfoToJSON()
        {
            string serializedSongInfo = JsonConvert.SerializeObject(fileState.songInfo);
            File.WriteAllText(fileState.FullFileName, serializedSongInfo);
        }

        public void OpenTranslation(string fileName)
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

        public void SongIsWritten_Handler(object sender, EventArgs e)
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

        public void TryRewriteSongInfo(string errorMessage)
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

        public void ImportXmlHandler(object sender, EventArgs e)
        {
            if (!AskPath(PathTypesToAsk.Xml, out string path))
            {
                return;
            }

            if (xmlSerializer == null)
            {
                xmlSerializer = new XmlSerializer(typeof(SongInfoWithTranslation));
            }
            xmlSerializer.Serialize(new FileStream(path, FileMode.Create), new SongInfoWithTranslation(fileState.songInfo, textField.Text));

        }


        public bool AskPath(PathTypesToAsk type, out string path)
        {
            string description;
            switch (type)
            {
                #region asking only directory name
                case PathTypesToAsk.TranslationsDirectory:
                    folderBrowserDialog.ShowDialog();
                    path = folderBrowserDialog.SelectedPath;
                    return true;
                #endregion

                #region asking the path with a file name
                case PathTypesToAsk.Xml:
                    saveFileDialog.FileName = fileState.FileNameWithoutExtension + ".xml";
                    saveFileDialog.Filter = ".xml files(*.xml)|*.xml";
                    break;
                    
                #endregion

                default:
                    throw new NotImplementedException();
            }
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog.FileName;
                return true;
            }
            path = null;
            return false;
        }

        internal void ChangeDirecrotyHandler(object sender, EventArgs e)
        {
            if (AskPath(PathTypesToAsk.TranslationsDirectory, out string folderPath))
            {
                SetInitialDirectory(folderPath);
            }
        }
    }
}