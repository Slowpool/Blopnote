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

        private string FilePath => directory.FullName + "\\" + fileCondition.FileName;
        private string LyricsPath => EditFilePathToLyricsPath();

        private string EditFilePathToLyricsPath()
        {
            string lyricsPath = FilePath.Insert(FilePath.Length - 4, " lyrics");
            int indexOfLastSlash = FilePath.LastIndexOf('\\');
            lyricsPath = lyricsPath.Insert(indexOfLastSlash, "\\lyrics");
            return lyricsPath;
        }

        internal FileProcessor(TextField textField, FileCondition fileCondition, LyricsBox lyricsBox, OpenFileDialog openFileDialog)
        {
            this.textField = textField;
            this.fileCondition = fileCondition;
            this.lyricsBox = lyricsBox;
            this.openFileDialog = openFileDialog;
        }

        // Q it doesn't matter for me now
        internal void ChangeDirectory(string directoryName)
        {
            directory = new DirectoryInfo(directoryName);
            openFileDialog.InitialDirectory = directoryName;
        }

        // Q it doesn't matter for me now
        //internal void CreateLyricsInCurrentDirectoryIfNeed()
        //{
        //    bool lyricsExists = (from dir in directory.GetDirectories()
        //                        where dir.Name == "lyrics"
        //                        select dir)
        //                        .Count() == 1;
        //    if (lyricsExists)
        //    {
        //        return;
        //    }
        //    else
        //    {

        //    }
        //}

        internal void CreateNewTranslation(string fileName, string lyrics)
        {
            PrepareTranslation(fileName, lyrics);

            File.Create(FilePath).Dispose();

            if (fileCondition.LyricsExists)
            {
                WriteLyrics(lyrics);
                lyricsBox.BuildNewLyrics(lyrics);
            }
            else
            {
                //  Q do I really need this?
                lyricsBox.NoLyrics();
            }
        }

        private void PrepareTranslation(string fileName, string lyrics)
        {
            fileCondition.FileName = fileName;
            fileCondition.LyricsExistCheck(lyrics);
            fileCondition.RefreshStatus();
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
            PrepareTranslation(fileName, lyrics);
            lyricsBox.BuildNewLyrics(lyrics);
            
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