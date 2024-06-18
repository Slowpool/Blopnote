using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Blopnote.Properties;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using static Blopnote.Browser;
using System.IO;

namespace Blopnote
{
    public partial class CreateNewTranslationForm : Form
    {
        private string SongName => TextBoxForAuthor.Text + " - " + TextBoxForSong.Text;
        internal FileInfo fileInfo { get; set; }
        internal SongInfo songInfo { get; set; }

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

        private readonly URL_item[] URL_items;
        private readonly string[,] ZERO_URLs = new string[,] { };
        private URL_item SelectedURL_item => URL_items.Where(item => item.Checked)
                                                      .Single();
        internal CreateNewTranslationForm()
        {
            InitializeComponent();
            Icon = Resources.icon;
            URL_items = new URL_item[]
            {
                new URL_item(buttonCopy1, radioButtonURL1, linkLabelURL1),
                new URL_item(buttonCopy2, radioButtonURL2, linkLabelURL2),
                new URL_item(buttonCopy3, radioButtonURL3, linkLabelURL3),
                new URL_item(buttonCopy4, radioButtonURL4, linkLabelURL4),
                new URL_item(buttonCopy5, radioButtonURL5, linkLabelURL5),
            };
        }

        [STAThread]
        internal DialogResult ShowForDataInput()
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
            UpdateLyricsSelector();

            ClearAllRelatedToURLs();

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
        }

        private void ClearAllRelatedToURLs()
        {
            DisplayURLs(ZERO_URLs);
            labelURL_Request.Text = string.Empty;
        }

        private void CheckBoxUseLyrics_CheckedChanged(object sender, EventArgs e)
        {
            TextBoxForLyrics.Enabled = checkBoxUseLyrics.Checked;
            buttonLyricsRequest.Enabled = checkBoxUseLyrics.Checked;
            labelLyricsRequestResult.Enabled = checkBoxUseLyrics.Checked;
            ClearAllRelatedToLyrics();
            UpdateLyricsSelector();
        }

        private void checkBoxStoreURL_CheckedChanged(object sender, EventArgs e)
        {
            buttonRequestForURL.Enabled = checkBoxUseURL.Checked;
            ClearAllRelatedToURLs();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.None))
            {
                songInfo = new SongInfo(lyrics: TextBoxForLyrics.Text, URL: SelectedURL_item.URL);
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

        private async void buttonLyricsRequest_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LyricsId = 0;
            if (SongInserted)
            {
                lyricsReferences = await RequestForSimilarSongs(SongName);
                if (lyricsReferences.Count == 0)
                {
                    ClearAllRelatedToLyrics();
                    UpdateLyricsSelector();
                    labelLyricsRequestResult.Text = NOT_FOUND_MESSAGE;
                }
                else
                {
                    labelLyricsRequestResult.Text = string.Format("{0} lyrics found", lyricsReferences.Count);
                    DownloadedLyrics.Clear();
                    LyricsId = 0;
                    LoadLyricsToTextBox();
                }
            }
            else
            {
                ClearAllRelatedToLyrics();
                UpdateLyricsSelector();
                labelLyricsRequestResult.Text = EMPTY_FIELDS_MESSAGE;
            }
            Cursor.Current = Cursors.Default;
        }

        internal async Task<string> GetLyrics(string GeniusSongURL)
        {
            driver.Navigate().GoToUrl(GeniusSongURL);
            await Task.Delay(1000);
            IEnumerable<IWebElement> divs = driver.FindElements(By.XPath("(//div[contains(@data-lyrics-container,'true')])"));
            return divs.Aggregate(string.Empty, (lyrics, div) => lyrics + div.Text);
            //// latch
            //return "haha";
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

        private async void LoadLyricsToTextBox()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                TextBoxForLyrics.Text = DownloadedLyrics[LyricsId];
            }
            catch
            {
                DownloadedLyrics.Add(await GetLyrics(lyricsReferences[LyricsId]));
                TextBoxForLyrics.Text = DownloadedLyrics[LyricsId];
            }
            UpdateLyricsSelector();
            Cursor.Current = Cursors.Default;
        }

        internal void UpdateMaxLength(int length)
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

        private void groupBoxURL_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (checkBoxUseURL.Checked && !SelectedURL_item.Visible)
            {
                e.Cancel = true;
                errorProvider1.SetError(linkLabelURL1, "You specified \"Use URL\" but didn't request it");
            }
        }
        #endregion

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string URL = URL_items.Where(URLitem => URLitem.HasButton(button))
                                  .Single()
                                  .Name;
            Clipboard.SetText(URL);
        }

        private async void buttonRequestForURL_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (SongInserted)
            {
                string[,] URLs = await Browser.GetYoutubeURLs(SongName);
                DisplayURLs(URLs);
                labelURL_Request.Text = URLs.GetLength(0) == 0
                                        ? NOT_FOUND_MESSAGE
                                        : URLs.GetLength(0) + " URLs were successfully found";
            }
            else
            {
                DisplayURLs(ZERO_URLs);
                labelURL_Request.Text = EMPTY_FIELDS_MESSAGE;
            }
            Cursor.Current = Cursors.Default;
        }

        private void DisplayURLs(string[,] URLs)
        {
            URL_items[0].Checked = true;
            for(int i = 0; i < URLs.GetLength(0); i++)
            {
                URL_items[i].Name = URLs[i, 0];
                URL_items[i].URL = URLs[i, 1];
                URL_items[i].Visible = true;
            }
            // hide url items without url
            for (int i = URLs.Length; i < URL_items.Length; i++)
            {
                URL_items[i].Name = string.Empty;
                URL_items[i].URL = string.Empty;
                URL_items[i].Visible = false;
            }
        }

        private void linkLabelURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string URL = GetURL((LinkLabel)sender);
            Browser.OpenURL(URL);
        }

        private string GetURL(LinkLabel linkLabel)
        {
            foreach(URL_item item in URL_items)
            {
                if (item.HasLabel(linkLabel))
                {
                    return item.URL;
                }
            }
            throw new ArgumentException("");
        }
    }
}
