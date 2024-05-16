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
        private readonly FileCondition fileCondition;
        private readonly LyricsBox lyricsBox;
        private readonly OpenFileDialog openFileDialog;

        private DirectoryInfo directory;
        internal event EventHandler DirectoryChanged;

        internal int DirectoryLength => directory.FullName.Length;

        private string FilePath => Path.Combine(directory.FullName, fileCondition.FileName);
        private string LyricsPath => EditFilePathToLyricsPath();

        private string EditFilePathToLyricsPath()
        {
#warning dirty and obfuscated
            string lyricsPath = FilePath.Insert(FilePath.Length - 4, " " + Names.LyricsFolder);
            int indexOfLastSlash = FilePath.LastIndexOf('\\');
            lyricsPath = lyricsPath.Insert(indexOfLastSlash, "\\" + Names.LyricsFolder);
            return lyricsPath;
        }

        internal FileProcessor(TextField textField, FileCondition fileCondition, LyricsBox lyricsBox, OpenFileDialog openFileDialog)
        {
            this.textField = textField;
            this.fileCondition = fileCondition;
            this.lyricsBox = lyricsBox;
            this.openFileDialog = openFileDialog;
        }

        internal void ChangeDirectory(string directoryName)
        {
            directory = new DirectoryInfo(directoryName);
            EnsureLyricsFolder();
            openFileDialog.InitialDirectory = directoryName;
            DirectoryChanged(this, null);
        }

        private void EnsureLyricsFolder() 
        {
            var subDirectories = (from dir in directory.GetDirectories()
                                  where dir.Name.ToLower() == Names.LyricsFolder
                                  select dir);
            try
            {
                EnsureNameInLowerCase(subDirectories.First());
            }
            catch (InvalidOperationException)
            {
                directory.CreateSubdirectory(Names.LyricsFolder);
            }
        }

        private void EnsureNameInLowerCase(DirectoryInfo lyricsDirectory)
        {
            if (lyricsDirectory == null)
            {
                throw new ArgumentException("directory can't be null");
            }

            if (lyricsDirectory.Name != Names.LyricsFolder)
            {
                lyricsDirectory.NameToLower();
            }
        }

        internal void CreateNewTranslation(string fileName, string lyrics)
        {
            fileCondition.PrepareTranslation(fileName, lyrics);

            File.Create(FilePath).Dispose();

            if (fileCondition.LyricsExists)
            {
                WriteLyrics(lyricsBox.BuildNewLyricsAndGetEditedVersion(lyrics));
            }
            else
            {
                //  Q do I really need this?
                lyricsBox.NoLyrics();
            }
        }

        internal void Save()
        {
            WriteInFile(FilePath, textField.Text);
        }

        private void WriteLyrics(string lyrics)
        {
            WriteInFile(LyricsPath, lyrics);
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
            string lyrics = FindLyricsOf(FullFileName);
            string fileName = FullFileName.Substring(FullFileName.LastIndexOf('\\') + 1);
            string directory = FullFileName.Substring(0, FullFileName.LastIndexOf('\\') + 1);
            ChangeDirectory(directory);
            fileCondition.PrepareTranslation(fileName, lyrics);
            ReadText(FullFileName);
            lyricsBox.BuildNewLyricsAndGetEditedVersion(lyrics);
            
        }

        private void ReadText(string FullFileName)
        {
            using(var reader = new StreamReader(FullFileName, Encoding.UTF8))
            {
                textField.Text = reader.ReadToEnd();
            }
        }

        private string FindLyricsOf(string FullFileName)
        {
            // C:\Users\azgel\Desktop\translations\ic3peak - no death.txt
            string lyricsPath = FullFileName.Insert(FullFileName.Length - 4, " lyrics").Insert(FullFileName.LastIndexOf('\\'), "\\lyrics");
            if (File.Exists(lyricsPath))
            {
                using(var reader = new StreamReader(path: lyricsPath, encoding: Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}