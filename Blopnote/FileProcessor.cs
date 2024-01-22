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
    public class FileProcessor
    {
        private readonly SaveFileDialog SaveFileDialog1;
        private readonly TextField textField;
        private readonly Title title;
        private readonly Condition condition;

        private bool FileExists => SaveFileDialog1.CheckFileExists;

        private bool FileJustCreated => condition.FileState == FileStates.JustCreated;

        public FileProcessor(TextField textField, Title title, Condition condition)
        {
            this.textField = textField;
            this.title = title;
            this.condition = condition;
            this.SaveFileDialog1 = new SaveFileDialog();
            this.SaveFileDialog1.Filter = "txt files (*.txt)|*.txt|all files (*.*)|*.*";
        }

        public void SaveFileAs()
        {
            DialogResult userAnswer = SaveFileDialog1.ShowDialog();
            if (userAnswer == DialogResult.OK)
            {
                WriteFile();
                if (condition.IsSaved())
                {
                    return;
                }
                else
                {
                    condition.FileState = FileStates.Saved;
                }
            }
        }

        public void SaveFile()
        {
            WriteFile();
            condition.FileState = FileStates.Saved;
            title.SetNewName(SaveFileDialog1.FileName);
        }

        public void WriteFile()
        {
            using (var writer = new StreamWriter(SaveFileDialog1.FileName, append: false, encoding: Encoding.UTF8))
            {
                writer.Write(textField.GetText());
            }
        }

        public void SaveIfNeed()
        {
            if (condition.FileHasName)
            {
                SaveFile();
            }
            else
            {
                SaveFileAs();
            }
        }
    }
}