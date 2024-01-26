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
        private readonly ToolStripStatusLabel programStatus;
        private readonly TextField textField;

        internal string FileName { get; set; }

        internal bool LyricsExists { get; set; }
        internal bool LyricsDisplayed { get; set; }

        internal FileCondition(ToolStripStatusLabel programStatus, TextField textField)
        {
            this.programStatus = programStatus;
            this.textField = textField;
        }

        internal void DoesNotExist()
        {
            FileName = null;
            textField.Disable();
            programStatus.Text = "Create or open any file";
        }

        internal void SwitchLyricsUsed()
        {
            LyricsDisplayed = !LyricsDisplayed;
        }

        internal void CheckLyrics(string lyrics)
        {
            if (string.IsNullOrEmpty(lyrics))
            {
                LyricsExists = false;
            }
            else
            {
                LyricsExists = true;
            }
        }
    }
}