using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Blopnote
{
    internal class FileCondition
    {
        private readonly ToolStripStatusLabel programStatus;
        private readonly TextField textField;


        internal string FileName { get; set; }
        internal bool LyricsExists { get; set; }

        internal FileCondition(ToolStripStatusLabel programStatus, TextField textField)
        {
            this.programStatus = programStatus;
            this.textField = textField;
        }

        internal void DoesNotExist()
        {
            FileName = null;
            textField.Clear();
            textField.Disable();
            programStatus.Text = "Create or open any file";
        }

        internal void PrepareTranslation(string newFileName, string newLyrics)
        {
            LyricsExists = !string.IsNullOrEmpty(newLyrics);
            FileName = newFileName;
            string fullSongName = FileName.Substring(0, FileName.LastIndexOf('.'));
            programStatus.Text = "Song: " + fullSongName;
        }
    }
}