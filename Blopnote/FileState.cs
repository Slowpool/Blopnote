using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Blopnote
{
    internal class FileState
    {
        private readonly ToolStripStatusLabel programStatus;
        private readonly TextField textField;
        internal SongInfo songInfo { get; set; }

        internal string FileName { get; set; }
        internal bool LyricsIsUsed { get; set; }

        internal FileState(ToolStripStatusLabel programStatus, TextField textField)
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

        internal void UpdateState(string fileName, string lyrics, DirectoryInfo directory)
        {
            LyricsIsUsed = !string.IsNullOrEmpty(lyrics);
            FileName = fileName;
            string fullSongName = FileName.Substring(0, FileName.LastIndexOf('.'));
            programStatus.Text = "Song: " + Path.Combine(directory.FullName, fullSongName);
        }

        internal void CreateSongInfo(string lyrics)
        {
            songInfo = LyricsIsUsed ? new SongInfo(lyrics) : null;
        }

        internal void ReadSongInfo(string conjectiveLyricsPath)
        {
            
            string text = File.ReadAllText(conjectiveLyricsPath);
//#error How to convert from (object)song.json to (SongInfo)songIngo?
            songInfo = (SongInfo)JsonConvert.DeserializeObject(text, typeof(SongInfo));
        }
    }
}