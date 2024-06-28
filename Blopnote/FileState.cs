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

        private SongInfo songInfo_; internal SongInfo songInfo
        {
            get => songInfo_;
            set
            {
                songInfo_ = value;
                UrlChanged(this, null);
                LyricsChanged(this, null);
            }
        }
        private bool songInfoExists => songInfo != null;

        internal event EventHandler UrlChanged;
        public string Url
        {
            get => songInfoExists ? songInfo.Url : null;
            set
            {
                if (songInfoExists)
                {
                    songInfo.Url = value;
                }
                else
                {
                    // latch
                    //MessageBox.Show("There was no song info");
                    songInfo = new SongInfo(null, value);
                }
                UrlChanged(this, null);
            }
        }
        internal bool IsUrlUsed => !string.IsNullOrEmpty(Url);

        internal event FileStateEventHandler FileOpenedOrClosed;
        internal delegate void FileStateEventHandler (object sender, FileStateEventArgs e);
        private FileInfo _fileInfo; internal FileInfo fileInfo
        {
            get => _fileInfo;
            set {
                _fileInfo = value;
                if (fileInfo == null)
                {
                    programStatus.Text = "Create or open any file";
                    FileOpenedOrClosed(this, new FileStateEventArgs(false));
                }
                else
                {
                    programStatus.Text = "Song: " + FullFileName;
                    FileOpenedOrClosed(this, new FileStateEventArgs(true));
                }
            }
        }

        internal DirectoryInfo directoryInfo { get; set; }

        internal string FileName => fileInfo.Name;
        internal string FullFileName => fileInfo.FullName;

        internal DirectoryInfo[] NestedFolders => directoryInfo.GetDirectories();

        internal event EventHandler LyricsChanged;
        public string Lyrics
        {
            get => songInfoExists ? songInfo.Lyrics : null;
            set
            {
                if (songInfoExists)
                {
                    songInfo.Lyrics = value;
                }
                else
                {
                    // latch
                    //MessageBox.Show("There was no song info");
                    songInfo = new SongInfo(value, null);
                }
                LyricsChanged(this, null);
            }
        }
        internal bool IsLyricsUsed => !string.IsNullOrEmpty(Lyrics);

        internal FileState(ToolStripStatusLabel programStatus, TextField textField)
        {
            this.programStatus = programStatus;
            this.textField = textField;
        }

        internal void DoesNotExist()
        {
            songInfo = null;
            fileInfo = null;

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
    }

    internal class FileStateEventArgs : EventArgs
    {
        internal readonly bool Opened;

        internal FileStateEventArgs(bool opened)
        {
            Opened = opened;
        }
    }
}