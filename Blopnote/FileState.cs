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
    public class FileState
    {
        private readonly ToolStripStatusLabel programStatus;
        private readonly TextField textField;

        private SongInfo songInfo_; public SongInfo songInfo
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

        public event EventHandler UrlChanged;
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
        public bool IsUrlUsed => !string.IsNullOrEmpty(Url);

        public event FileStateEventHandler FileOpenedOrClosed;
        public delegate void FileStateEventHandler (object sender, FileStateEventArgs e);
        private FileInfo _fileInfo; public FileInfo fileInfo
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

        public DirectoryInfo directoryInfo { get; set; }

        public string FileName => fileInfo.Name;
        public string FullFileName => fileInfo.FullName;

        public DirectoryInfo[] NestedFolders => directoryInfo.GetDirectories();

        public event EventHandler LyricsChanged;
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
        public bool IsLyricsUsed => !string.IsNullOrEmpty(Lyrics);

        public FileState(ToolStripStatusLabel programStatus, TextField textField)
        {
            this.programStatus = programStatus;
            this.textField = textField;
        }

        public void DoesNotExist()
        {
            songInfo = null;
            fileInfo = null;

            textField.Disable();
        }

        public void NewFileInCurrentDir(FileInfo fileInfo, SongInfo songInfo)
        {
            this.fileInfo = new FileInfo(Path.Combine(directoryInfo.FullName, fileInfo.Name));
            this.songInfo = songInfo;
        }

        public void OpenFileWithDir(string fullFileName)
        {
            fileInfo = new FileInfo(fullFileName);
            directoryInfo = fileInfo.Directory;
        }
    }

    public class FileStateEventArgs : EventArgs
    {
        public readonly bool Opened;

        public FileStateEventArgs(bool opened)
        {
            Opened = opened;
        }
    }
}