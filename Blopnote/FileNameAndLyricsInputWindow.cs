using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Blopnote
{
    internal partial class FileNameAndLyricsInputWindow : Form
    {
        internal string FileName => Author + " - " + Song;

        private string Author { get; set; }
        private string Song { get; set; }

        #warning how can i handle lyrics here
        internal string Lyrics { get; set; }

        internal bool IsDataInserted => !string.IsNullOrEmpty(Author)
                                     && !string.IsNullOrEmpty(Song);

        internal FileNameAndLyricsInputWindow()
        {
            InitializeComponent();
        }

        [STAThread]
        internal void ShowForDataInput()
        {
            ResetAllComponents();

            DialogResult userAnswer = this.ShowDialog();

            if (userAnswer == DialogResult.OK)
            {
                Author = TextBoxForAuthor.Text;
                Song = TextBoxForSong.Text;
                Lyrics = TextBoxForLyrics.Text;
            }
        }

        private void ResetAllComponents()
        {
            Author = null;
            Song = null;
            Lyrics = null;
            TextBoxForAuthor.Clear();
            TextBoxForSong.Clear();
            TextBoxForLyrics.Clear();
            CheckBoxUseLyrics.Checked = true;
        }

        private void CheckBoxUseLyrics_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxUseLyrics.Checked)
            {
                TextBoxForLyrics.Enabled = true;
            }
            else
            {
                TextBoxForLyrics.Enabled = false;
                TextBoxForLyrics.Clear();
            }
        }

        private void FileNameAndLyricsInputWindow_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void OK_Click(object sender, EventArgs e)
        {

        }
    }
}