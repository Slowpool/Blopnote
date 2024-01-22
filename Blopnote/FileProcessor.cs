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
        private readonly Condition condition;

        private bool FileExists => SaveFileDialog1.CheckFileExists;

        public FileProcessor(TextField textField, Condition condition)
        {
            this.textField = textField;
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
            }
        }

        public void SaveFile()
        {
            WriteFile();
        }

        public void WriteFile()
        {
            using (var writer = new StreamWriter(SaveFileDialog1.FileName, append: false, encoding: Encoding.UTF8))
            {
                writer.Write(textField.GetText());
            }
        }
    }
}