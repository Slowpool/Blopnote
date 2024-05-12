using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Blopnote.Properties;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using SeleniumKeys = OpenQA.Selenium.Keys;

namespace Blopnote
{
    public partial class FileNameAndLyricsInputWindow : Form
    {
        internal string FileName => Author + " - " + Song + ".txt";

        private string Author { get; set; }
        private string Song { get; set; }

        internal string Lyrics { get; set; }

        internal bool IsDataInserted => !string.IsNullOrEmpty(Author)
                                     && !string.IsNullOrEmpty(Song);

        private ChromeDriver driver;
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

        internal FileNameAndLyricsInputWindow()
        {
            InitializeComponent();
            Icon = Resources.icon;
        }

        private void FileNameAndLyricsInputWindow_Load(object sender, EventArgs e)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless"); // Hide the browser window
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-gpu"); // Disable hardware acceleration.
            options.PageLoadStrategy = PageLoadStrategy.Eager;

            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            driver = new ChromeDriver(service, options);
            driver.Manage().Window.Maximize();
        }

        [STAThread]
        internal void ShowForDataInput()
        {
            ClearAll();

            DialogResult userAnswer = this.ShowDialog();
            if (userAnswer == DialogResult.OK)
            {
                Author = TextBoxForAuthor.Text;
                Song = TextBoxForSong.Text;
                ReadLyricsIfItIsUsed();
            }
        }

        private void ClearAll()
        {
            Author = null;
            Song = null;
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
            labelRequestResult.Text = string.Empty;
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
            labelRequestResult.Enabled = CheckBoxUseLyrics.Checked;
            ClearAllRelatedToLyrics();
            UpdateLyricsSelector();
        }

        private void ReadLyricsIfItIsUsed()
        {
            if (CheckBoxUseLyrics.Checked)
            {
                Lyrics = TextBoxForLyrics.Text;
            }
        }

        private void FileNameAndLyricsInputWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            driver.Close();
            driver.Dispose();
        }

        private void OK_Click(object sender, EventArgs e)
        {

        }

        private void TextBoxForAuthor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-')
            {
                e.Handled = true;
            }
        }

        private async void buttonSearchOnGenius_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LyricsId = 0;
            if (TextBoxForAuthor.Text.Length != 0 && TextBoxForSong.Text.Length != 0)
            {
                string songName = TextBoxForAuthor.Text + " " + TextBoxForSong.Text;
                references = await RequestForSimilarSongs(songName);
                if (references.Count == 0)
                {
                    labelRequestResult.Text = "Lyrics wasn't found";
                }
                else
                {
                    labelRequestResult.Text = string.Format("{0} lyrics were successfully found.", references.Count);
                    DownloadedLyrics.Clear();
                    LyricsId = 0;
                    LoadLyricsToTextBox();
                }
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
    }
}
