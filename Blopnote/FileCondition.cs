using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Blopnote
{
    internal class FileCondition
    {
        private readonly ToolStripStatusLabel status;
        private readonly TextField textField;

        internal string FileName { get; set; }
        internal bool FileHasName => FileName != null;

        internal FileCondition(ToolStripStatusLabel status, TextField textField)
        {
            this.status = status;
            this.textField = textField;
        }

        internal void DoesNotExist()
        {
            FileName = null;
            textField.Disable();
            status.Text = "Create or open any file";
        }

        
    }
}