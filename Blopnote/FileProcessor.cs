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
        private readonly FileCondition condition;
        private readonly LyricsBox lyricsBox;

        private DirectoryInfo directory;

        internal FileProcessor(TextField textField, FileCondition condition)
        {
            this.textField = textField;
            this.condition = condition;
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
            directory = new DirectoryInfo(fileName);
            WriteText();

            // TODO

        }

        internal void WriteText()
        {
            using (var writer = new StreamWriter(condition.FileName, append: false, encoding: Encoding.UTF8))
            {
                writer.Write(textField.GetText());
            }
        }
    }
}