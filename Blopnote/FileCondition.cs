﻿using System;
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

        private string fileName;
        internal string FileName
        {
            get => fileName;
            set
            {
                fileName = value;
            }
        }

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

        internal void LyricsExistCheck(string lyrics)
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

        internal void RefreshStatus()
        {
            int indexOfLastPoint = FileName.LastIndexOf('.');
            string song = FileName.Substring(0, indexOfLastPoint);
            programStatus.Text = "Song: " + song;
        }
    }
}