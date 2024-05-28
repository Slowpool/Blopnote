using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Blopnote.Properties;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using SeleniumKeys = OpenQA.Selenium.Keys;
using static Blopnote.Browser;

namespace Blopnote
{
    public partial class CreateNewTranslationWindow : Form
    {
        private string Author => TextBoxForAuthor.Text;
        private string Song => TextBoxForSong.Text;
        internal string Lyrics { get; set; }

        private string SongAuthor => Author + " - " + Song;
        internal string FileName { get; set; }

        private bool SongInserted => TextBoxForAuthor.Text.Length != 0 && TextBoxForSong.Text.Length != 0;

        private bool AuthorIsCorrect => !string.IsNullOrEmpty(Author);
        private bool SongIsCorrect => !string.IsNullOrEmpty(Song);
        private bool LyricsIsCorrect => !(CheckBoxUseLyrics.Checked ^ !string.IsNullOrEmpty(Lyrics));

        internal bool InsertedDataIsComplete => AuthorIsCorrect
                                             && LyricsIsCorrect
                                             && SongIsCorrect;

        private const string FIELD_X_IS_EMPTY = "Field \"{0}\" is empty.\n";
#warning what if i use mac? it has to be gotten dinamically
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

        internal CreateNewTranslationWindow()
        {
            InitializeComponent();
            Icon = Resources.icon;
        }

        private void FileNameAndLyricsInputWindow_Load(object sender, EventArgs e)
        {
            
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

        private void CheckBoxUseLyrics_CheckedChanged(object sender, EventArgs e)
        {
            TextBoxForLyrics.Enabled = CheckBoxUseLyrics.Checked;
            buttonLyricsRequest.Enabled = CheckBoxUseLyrics.Checked;
            labelLyricsRequestResult.Enabled = CheckBoxUseLyrics.Checked;
            ClearAllRelatedToLyrics();
            UpdateLyricsSelector();
        }

        private void FileNameAndLyricsInputWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            driver.Close();
            driver.Dispose();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            Lyrics = TextBoxForLyrics.Text;
            FileName = SongAuthor + ".txt";

            if (!InsertedDataIsComplete)
            {
#warning is it dry violation?
                string messageText = AuthorIsCorrect ? string.Empty : string.Format(FIELD_X_IS_EMPTY, "Author");
                messageText += SongIsCorrect ? string.Empty : string.Format(FIELD_X_IS_EMPTY, "Song");
                messageText += LyricsIsCorrect ? string.Empty : string.Format(FIELD_X_IS_EMPTY, "Lyrics");
                MessageBox.Show(caption: "Incomplite data",
                    text: messageText);
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
                references = await RequestForSimilarSongs(SongAuthor);
                if (references.Count == 0)
                {
                    ClearAllRelatedToLyrics();
                    UpdateLyricsSelector();
                    labelLyricsRequestResult.Text = "Not found";
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
            }
            Cursor.Current = Cursors.Default;
        }

        private async Task<List<string>> RequestForSimilarSongs(string songName)
        {
            Cursor.Current = Cursors.WaitCursor;
            driver.Navigate().GoToUrl("http://Genius.com");
            var query = driver.FindElement(By.Name("q"));
            query.Clear();
            query.SendKeys(songName);
            query.SendKeys(SeleniumKeys.Return);
            await Task.Delay(1000);

            IEnumerable<IWebElement> cards = driver.FindElements(By.ClassName("mini_card"))
                              .Skip(1)
                              .Take(5);

            List<string> references = new List<string>();
            string reference;
            foreach (var card in cards)
            {
                reference = card.GetAttribute("href");
                if (!references.Contains(reference))
                    references.Add(reference);
            }
            Cursor.Current = Cursors.Default;
            return references;
        }

        internal async Task<string> GetLyrics(string GeniusSongURL)
        {
            driver.Navigate().GoToUrl(GeniusSongURL);
            await Task.Delay(1000);
            IEnumerable<IWebElement> divs = driver.FindElements(By.XPath("(//div[contains(@data-lyrics-container,'true')])"));
            return divs.Aggregate(string.Empty, (lyrics, div) => lyrics + div.Text);
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
    }
}
