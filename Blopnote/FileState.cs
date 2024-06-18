using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blopnote
{
    internal class FileState
    {
        private readonly ToolStripStatusLabel programStatus;
        private readonly TextField textField;

        internal SongInfo songInfo { get; set; }

        private FileInfo _fileInfo; internal FileInfo fileInfo
        {
            get => _fileInfo;
            set
            {
                _fileInfo = value;
                if (fileInfo == null)
                {
                    programStatus.Text = "Create or open any file";
                }
                else
                {
                    programStatus.Text = "Song: " + FullFileName;
                }
            }
        }

        internal DirectoryInfo directoryInfo { get; set; }

        internal string FileName => fileInfo.Name;
        internal string FullFileName => fileInfo.FullName;
        internal string FilePath => Path.Combine(directoryInfo.FullName, fileInfo.Name);

        internal bool LyricsIsUsed => songInfo != null && !string.IsNullOrEmpty(songInfo.Lyrics);
        internal bool URL_IsUsed => !string.IsNullOrEmpty(songInfo.URL);

        public DirectoryInfo[] NestedFolders => directoryInfo.GetDirectories();

        internal FileState(ToolStripStatusLabel programStatus, TextField textField)
        {
            this.programStatus = programStatus;
            this.textField = textField;
        }

        internal void DoesNotExist()
        {
            songInfo = null;
            fileInfo = null;

            textField.Clear();
            textField.Disable();
        }

        internal void NewFileInCurrentDir(FileInfo fileInfo, SongInfo songInfo)
        {
            this.fileInfo = new FileInfo(Path.Combine(directoryInfo.FullName, fileInfo.Name));
            this.songInfo = songInfo;
        }

        internal void OpenFileWithDir(string fullFileName)
        {
            fileInfo = new FileInfo(fullFileName);
            directoryInfo = fileInfo.Directory;
        }

        internal void UpdateLyrics(string newLyrics)
        {
            songInfo.Lyrics = newLyrics;
        }
    }
}