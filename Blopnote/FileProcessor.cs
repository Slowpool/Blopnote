using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blopnote
{
    public class FileProcessor
    {
        private readonly SaveFileDialog SaveFileDialog1;
        private readonly TextField textField;
        private readonly Title title;

        private bool FileExists => SaveFileDialog1.CheckFileExists;

        private bool FileJustCreated => title.FileState == FileStates.JustCreated;

        public FileProcessor(TextField textField, Title title)
        {
            this.textField = textField;
            this.title = title;
            this.SaveFileDialog1 = new SaveFileDialog();
            this.SaveFileDialog1.Filter = "txt files (*.txt)|*.txt|all files (*.*)|*.*";
        }

        public void SaveFileAs()
        {
            DialogResult userAnswer = SaveFileDialog1.ShowDialog();
            if (userAnswer == DialogResult.OK)
            {
                WriteFile();
                title.FileState = FileStates.Saved;
            }
        }

        public void WriteFile()
        {
            using (var writer = new StreamWriter(SaveFileDialog1.FileName, append: false, encoding: Encoding.UTF8))
            {
                writer.Write(textField.GetText()); 
            }
        }

        public void SaveFile()
        {
            if (FileExists)
            {
                WriteFile();
                title.FileState = FileStates.Saved;
            }
            else
            {
                SaveFileAs();
            }
        }
    }
}