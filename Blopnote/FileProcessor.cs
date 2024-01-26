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

        internal FileProcessor(TextField textField, FileCondition condition)
        {
            this.textField = textField;
            this.fileCondition = condition;
        }

        // Q it doesn't matter for me now
        internal void ChangeDirectory(string directoryName)
        {
            directory = new DirectoryInfo(directoryName);
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
            fileCondition.FileName = fileName;
            File.Create(FilePath);

            fileCondition.CheckLyrics(lyrics);
            if (fileCondition.LyricsExists)
            {
                File.Create(LyricsPath);
                WriteLyrics(lyrics);
            }
        }

        private void WriteText()
        {
            WriteInFile(FilePath, textField.GetText());
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
    }
}