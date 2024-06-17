using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Blopnote.Properties;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using static Blopnote.Browser;

namespace Blopnote
{
    public partial class CreateNewTranslationForm : Form
    {
        private string Author => TextBoxForAuthor.Text;
        private string Song => TextBoxForSong.Text;
        internal string Lyrics { get; set; }
        internal string URL { get; set; }

        private string SongName => Author + " - " + Song;
        internal string FileName { get; set; }

        private bool SongInserted => TextBoxForAuthor.Text.Length != 0 && TextBoxForSong.Text.Length != 0;
        private const string EMPTY_FIELDS_MESSAGE = "Author or song name isn't inserted";
        private const string NOT_FOUND_MESSAGE = "Not found";

        private bool AuthorIsCorrect => !string.IsNullOrEmpty(Author);
        private bool SongIsCorrect => !string.IsNullOrEmpty(Song);
        private bool LyricsIsCorrect => !(CheckBoxUseLyrics.Checked ^ !string.IsNullOrEmpty(Lyrics));

        internal bool InsertedDataIsComplete => AuthorIsCorrect
                                             && LyricsIsCorrect
                                             && SongIsCorrect;

        private const string FIELD_X_IS_EMPTY = "Field \"{0}\" is empty.\n";

#warning what if i use mac? it has to be gotten dinamically
        // P.S. I meant not a mac but I've seen that max length of path can be changed manually in settings
        // in "pro-like" version of win10 to e.g. 500 characters. But I'm not sure 
        private const int PATH_MAX = 255;

        private List<string> references = new List<string>();
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
            CheckBoxUseLyrics.Checked = true;

            ClearAllRelatedToLyrics();
            UpdateLyricsSelector();

            ClearAllRelatedToURLs();
        }

        private void UpdateLyricsSelector()
        {
            buttonPreviousLyrics.Enabled = CheckBoxUseLyrics.Checked && LyricsId > 0;
            buttonNextLyrics.Enabled = CheckBoxUseLyrics.Checked && LyricsId < references.Count - 1;
        }

        private void ClearAllRelatedToLyrics()
        {
            labelLyricsRequestResult.Text = string.Empty;
            Lyrics = null;
#warning magic const
            LyricsId = -1;
            references.Clear();
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
            TextBoxForLyrics.Enabled = CheckBoxUseLyrics.Checked;
            buttonLyricsRequest.Enabled = CheckBoxUseLyrics.Checked;
            labelLyricsRequestResult.Enabled = CheckBoxUseLyrics.Checked;
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
            //if (ValidateChildren(ValidationConstraints.None))
            //{

            //    Lyrics = TextBoxForLyrics.Text;
            //    FileName = SongName + ".txt";
            //}
            //else
            //{
            //    this.DialogResult = DialogResult.None;
            //}

            Lyrics = TextBoxForLyrics.Text;
            FileName = SongName + ".txt";
            URL = URL_items.Where(item => item.Checked)
                           .Single()
                           .URL;
            if (!InsertedDataIsComplete)
            {
                // it's painfully
                string messageText = AuthorIsCorrect ? string.Empty : string.Format(FIELD_X_IS_EMPTY, "Author");
                messageText += SongIsCorrect ? string.Empty : string.Format(FIELD_X_IS_EMPTY, "Song");
                messageText += LyricsIsCorrect ? string.Empty : string.Format(FIELD_X_IS_EMPTY, "Lyrics");
#warning here I stopped
                //messageText += URL_IsCorrect ? string.Empty : string.Format(FIELD_X_IS_EMPTY, "URL");
                MessageBox.Show(caption: "Incomplite data",
                                   text: messageText);
// Q: wait, how does it work? why DialogResult after this line is still DialogResult.OK?
// A: get it. this is dialog result of form, not one of button. It's funny that I had forgotten how does my own code work
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
                references = await RequestForSimilarSongs(SongName);
                if (references.Count == 0)
                {
                    ClearAllRelatedToLyrics();
                    UpdateLyricsSelector();
                    labelLyricsRequestResult.Text = NOT_FOUND_MESSAGE;
                }
                else
                {
                    labelLyricsRequestResult.Text = string.Format("{0} lyrics found", references.Count);
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
                DownloadedLyrics.Add(await GetLyrics(references[LyricsId]));
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
        private void TextBoxAuthorAndSong_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox.Text.Length == 0)
            {
                errorProvider1.SetError(textBox, "This field can't be empty");
                e.Cancel = true;
            }
        }

        private void TextBoxAuthorAndSong_Validated(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            errorProvider1.SetError(textBox, "");
        }

        private void TextBoxForLyrics_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CheckBoxUseLyrics.Checked)
            {
                if (TextBoxForLyrics.Text.Length == 0)
                {
                    e.Cancel = true;
                    errorProvider1.SetError(TextBoxForLyrics, "You signed \"Use lyrics\" but didn't insert it");
                }
            }
        }

        private void TextBoxForLyrics_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(TextBoxForLyrics, "");
        }

        private void CreateNewTranslationForm_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void groupBoxSong_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void groupBoxSong_Validated(object sender, EventArgs e)
        {

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
            var info = new System.Diagnostics.ProcessStartInfo
            {
                FileName = URL,
                CreateNoWindow = false
            };
            System.Diagnostics.Process.Start(info);
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
