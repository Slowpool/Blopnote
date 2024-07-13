using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Blopnote.Properties;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.IO;

namespace Blopnote
{
    public partial class CreateNewTranslationForm : Form
    {
        private string SongName => TextBoxForAuthor.Text + " - " + TextBoxForSong.Text;
        public FileInfo fileInfo { get; set; }
        public SongInfo songInfo { get; set; }

        private bool SongInserted => ValidateTextBox(TextBoxForAuthor) && ValidateTextBox(TextBoxForSong);
        private const string EMPTY_FIELDS_MESSAGE = "Author or song name isn't inserted";
        private const string NOT_FOUND_MESSAGE = "Not found";

#warning what if i use mac? it has to be gotten dinamically
        // P.S. I meant not a mac but I've seen that max length of path can be changed manually in settings
        // in "pro-like" version of win10 to e.g. 500 characters. But I'm not sure 
        private const int PATH_MAX = 255;

        private List<string> lyricsReferences = new List<string>();
        private readonly List<string> DownloadedLyrics = new List<string>();

        private int lyricsId;

        private int LyricsId
        {
            get => lyricsId;
            set
            {
                lyricsId = value;
                if (LyricsId < DownloadedLyrics.Count)
                {
                    UpdateLyricsSelector();
                }
            }
        }

        private readonly Urlitem[] Urlitems;
        private readonly string[,] ZERO_Urls = new string[,] { };
        private Urlitem SelectedUrlitem => Urlitems.Where(item => item.Checked)
                                                   .Single();
        public CreateNewTranslationForm()
        {
            InitializeComponent();
            Icon = Resources.icon;
            Urlitems = new Urlitem[]
            {
                new Urlitem(buttonCopy1, radioButtonUrl1, linkLabelUrl1),
                new Urlitem(buttonCopy2, radioButtonUrl2, linkLabelUrl2),
                new Urlitem(buttonCopy3, radioButtonUrl3, linkLabelUrl3),
                new Urlitem(buttonCopy4, radioButtonUrl4, linkLabelUrl4),
                new Urlitem(buttonCopy5, radioButtonUrl5, linkLabelUrl5),
            };

            buttonLyricsRequest.Click += SetWaitingCursor;
            buttonLyricsRequest.Click += buttonLyricsRequest_Click;
            buttonLyricsRequest.Click += SetDefaultCursor;

            buttonNextLyrics.Click += SetWaitingCursor;
            buttonNextLyrics.Click += buttonNextLyrics_Click;
            buttonNextLyrics.Click += SetDefaultCursor;

            buttonRequestForUrl.Click += SetWaitingCursor;
            buttonRequestForUrl.Click += buttonRequestForUrl_Click;
            buttonRequestForUrl.Click += SetDefaultCursor;
        }

        [STAThread]
        public DialogResult ShowForDataInput()
        {
            ClearAll();
            return ShowDialog();
        }

        private void ClearAll()
        {
            TextBoxForAuthor.Clear();
            TextBoxForSong.Clear();
            checkBoxUseLyrics.Checked = true;

            ClearAllRelatedToLyrics();

            ClearAllRelatedToUrls();

            fileInfo = null;
            songInfo = null;
        }

        private void UpdateLyricsSelector()
        {
            buttonPreviousLyrics.Enabled = checkBoxUseLyrics.Checked && LyricsId > 0;
            buttonNextLyrics.Enabled = checkBoxUseLyrics.Checked && LyricsId < lyricsReferences.Count - 1;
        }

        private void ClearAllRelatedToLyrics()
        {
            labelLyricsRequestResult.Text = string.Empty;
#warning magic const
            LyricsId = -1;
            lyricsReferences.Clear();
            TextBoxForLyrics.Clear();
            DownloadedLyrics.Clear();
            UpdateLyricsSelector();
        }

        private void ClearAllRelatedToUrls()
        {
            DisplayUrls(ZERO_Urls);
            labelUrlRequest.Text = string.Empty;
        }

        private void SetWaitingCursor(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        private void SetDefaultCursor(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
        }

        private void CheckBoxUseLyrics_CheckedChanged(object sender, EventArgs e)
        {
            TextBoxForLyrics.Enabled = checkBoxUseLyrics.Checked;
            buttonLyricsRequest.Enabled = checkBoxUseLyrics.Checked;
            labelLyricsRequestResult.Enabled = checkBoxUseLyrics.Checked;
            ClearAllRelatedToLyrics();
        }

        private void checkBoxStoreUrlCheckedChanged(object sender, EventArgs e)
        {
            buttonRequestForUrl.Enabled = checkBoxUseUrl.Checked;
            ClearAllRelatedToUrls();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.None))
            {
                songInfo = new SongInfo(lyrics: TextBoxForLyrics.Text, Url: SelectedUrlitem.Url);
                fileInfo = new FileInfo(SongName + ".txt");
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
        }

        private void TextBoxForAuthor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-')
            {
                e.Handled = true;
            }
        }

        private void buttonLyricsRequest_Click(object sender, EventArgs e)
        {
            LyricsId = 0;
            if (!SongInserted)
            {
                ClearAllRelatedToLyrics();
                labelLyricsRequestResult.Text = EMPTY_FIELDS_MESSAGE;
                return;
            }

            TryActWithBrowserOtherwiseShowErrorMessage(delegate
            {
                lyricsReferences = Browser.Instance.RequestForSimilarSongs(SongName);
            });
            

            if (lyricsReferences.Count == 0)
            {
                ClearAllRelatedToLyrics();
                labelLyricsRequestResult.Text = NOT_FOUND_MESSAGE;
                return;
            }

            labelLyricsRequestResult.Text = string.Format($"{lyricsReferences.Count} lyrics found");
            DownloadedLyrics.Clear();

            TryActWithBrowserOtherwiseShowErrorMessage(delegate
            {
                LoadLyricsToTextBox();
            });
        }

        private void TryActWithBrowserOtherwiseShowErrorMessage(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception exception)
            {
                MessageShower.Show(exception);
                return;
            }
        }

        private void buttonNextLyrics_Click(object sender, EventArgs e)
        {
            LyricsId++;
            LoadLyricsToTextBox();
        }

        private void buttonPreviousLyrics_Click(object sender, EventArgs e)
        {
            LyricsId--;
            LoadLyricsToTextBox();
        }

        private void LoadLyricsToTextBox()
        {
            try
            {
                TextBoxForLyrics.Text = DownloadedLyrics[LyricsId];
            }
            catch
            {
                DownloadedLyrics.Add(Browser.Instance.FindLyrics(lyricsReferences[LyricsId]));
                TextBoxForLyrics.Text = DownloadedLyrics[LyricsId];
            }
            UpdateLyricsSelector();
        }

        public void UpdateMaxLength(int length)
        {
            // length is
            // |_____________|
            // path/to/folder/future_file_name.txt
            int fieldMaxLength = PATH_MAX - length;
            // so now authorMaxLength is
            // /future_file_name.txt which includes Author and Song
            // which are joined with three characters: " - "
            fieldMaxLength -= 3;
            // /future_file_name.txt => future_file_name.txt
            fieldMaxLength -= 1;
            // and ".txt" at the end, so we must subtract 3 and then 4
            fieldMaxLength -= 4;
            // also path/to/folder has folder lyrics/ which adds 7 characters
            fieldMaxLength -= 7;
            // and then in lyrics folder could be future_file_name lyrics.txt
            // which adds to length 7 characters so subtract them also
            fieldMaxLength -= 7;
            // than we must divide by 2 because filename is author_name + song_name where both
            // had better to have the same length
            fieldMaxLength /= 2;
            TextBoxForAuthor.MaxLength = fieldMaxLength;
            TextBoxForSong.MaxLength = fieldMaxLength;
        }

        #region Validating
        private bool ValidateTextBox(TextBox textBox)
        {
            errorProvider1.SetError(textBox, textBox.Text.Length != 0 ? "" : "This field can't be empty");
            return textBox.Text.Length != 0;
        }

        private void groupBoxSong_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ValidateTextBox(TextBoxForAuthor) || !ValidateTextBox(TextBoxForSong))
            {
                e.Cancel = true;
            }
        }

        private void groupBoxLyrics_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (checkBoxUseLyrics.Checked && TextBoxForLyrics.Text.Length == 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(TextBoxForLyrics, "You specified \"Use lyrics\" but didn't insert it");
            }
            else
            {
                errorProvider1.SetError(TextBoxForLyrics, "");
            }
        }

        private void groupBoxUrlValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (checkBoxUseUrl.Checked && !SelectedUrlitem.Visible)
            {
                e.Cancel = true;
                errorProvider1.SetError(linkLabelUrl1, "You specified \"Use Url\" but didn't request it");
            }
        }
        #endregion

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string Url = Urlitems.Where(Urlitem => Urlitem.HasButton(button))
                                  .Single()
                                  .Name;
            Clipboard.SetText(Url);
        }

        private void buttonRequestForUrl_Click(object sender, EventArgs e)
        {
            if (SongInserted)
            {
                TryActWithBrowserOtherwiseShowErrorMessage(delegate
                {
                    string[,] Urls = Browser.Instance.GetYoutubeUrls(SongName);
                    DisplayUrls(Urls);

                    labelUrlRequest.Text = Urls.GetLength(0) == 0
                                        ? NOT_FOUND_MESSAGE
                                        : Urls.GetLength(0) + " Urls were found";
                });
            }
            else
            {
                DisplayUrls(ZERO_Urls);
                labelUrlRequest.Text = EMPTY_FIELDS_MESSAGE;
            }
        }

        private void DisplayUrls(string[,] Urls)
        {
            Urlitems[0].Checked = true;
            for(int i = 0; i < Urls.GetLength(0); i++)
            {
                Urlitems[i].Name = Urls[i, 0];
                Urlitems[i].Url = Urls[i, 1];
                Urlitems[i].Visible = true;
            }
            // hide url items without url
            for (int i = Urls.Length; i < Urlitems.Length; i++)
            {
                Urlitems[i].Name = string.Empty;
                Urlitems[i].Url = string.Empty;
                Urlitems[i].Visible = false;
            }
        }

        private void linkLabelUrlLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Browser.OpenUrlForUser(GetUrl((LinkLabel)sender));
        }

        private string GetUrl(LinkLabel linkLabel)
        {
            foreach(Urlitem item in Urlitems)
            {
                if (item.HasLabel(linkLabel))
                {
                    return item.Url;
                }
            }
            throw new ArgumentException("");
        }

        private void browserToolStripMenuItem_Click(object sender, EventArgs e)
        {
#warning dry browser
            try
            {
                Browser.Instance.DoNothing();
            }
            catch
            {
                MessageShower.Show(new Exception("Failed to reconnect. Make sure you have an internet?"));
            }
        }
    }
}
