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

        private string directoryPath { get; set; }

        internal FileProcessor(TextField textField, FileCondition condition)
        {
            this.textField = textField;
            this.condition = condition;
        }

        internal void ChangeDirectory(string directoryName)
        {
            directoryPath = directoryName;
        }

        internal void CreateNewFile(string fileName)
        {
            #warning
        }

        internal void WriteFile()
        {
            using (var writer = new StreamWriter(condition.FileName, append: false, encoding: Encoding.UTF8))
            {
                writer.Write(textField.GetText());
            }
        }
    }
}