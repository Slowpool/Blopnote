using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using SeleniumKeys = OpenQA.Selenium.Keys;
using System.Diagnostics;

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
        private int lyricsId;
        private List<string> references;
        private readonly List<string> DownloadedLyrics = new List<string>();
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

        private void UpdateLyricsSelector()
        {
            buttonPreviousLyrics.Enabled = lyricsId != 0;
            buttonNextLyrics.Enabled = lyricsId != references.Count - 1;
        }

        internal FileNameAndLyricsInputWindow()
        {
            InitializeComponent();
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
            ResetAllComponents();
            TextBoxForAuthor.Text = "dekma";
            TextBoxForSong.Text = "меня нет";

            DialogResult userAnswer = this.ShowDialog();

            if (userAnswer == DialogResult.OK)
            {
                Author = TextBoxForAuthor.Text;
                Song = TextBoxForSong.Text;
                ReadLyricsIfItUsed();
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

        private void ReadLyricsIfItUsed()
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
            LyricsId = 0;
            if (TextBoxForAuthor.Text.Length != 0 && TextBoxForSong.Text.Length != 0)
            {
                string songName = TextBoxForAuthor.Text + " " + TextBoxForSong.Text;
                references = await RequestForSimilarSongs(songName);
                if (references.Count == 0)
                {
                    buttonNextLyrics.Visible = false;
                    labelSearchResult.Text = "Song wasn't found";
                }
                else
                {
                    buttonNextLyrics.Visible = true;
                    labelSearchResult.Text = string.Format("Successfuly find {0} lyrics", references.Count);
                    DownloadedLyrics.Clear();
                    LyricsId = 0;
                    DisplayLyrics();
                }
            }
        }

        private async Task<List<string>> RequestForSimilarSongs(string songName)
        {
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
            return references;
        }

        internal async Task<string> GetLyrics(string GeniusSongURL)
        {
            driver.Navigate().GoToUrl(GeniusSongURL);
            await Task.Delay(1000);
            IEnumerable<IWebElement> divs = driver.FindElements(By.XPath("(//div[contains(@data-lyrics-container,'true')])"));
            string lyrics = string.Empty;
            foreach (var div in divs)
            {
                lyrics += GetPartOfLyrics(div);
            }
            return lyrics;
        }

        internal string GetPartOfLyrics(IWebElement div)
        {
            return div.Text;
        }

        private void buttonNextLyrics_Click(object sender, EventArgs e)
        {
            LyricsId++;
            DisplayLyrics();
        }

        private void buttonPreviousLyrics_Click(object sender, EventArgs e)
        {
            LyricsId--;
            DisplayLyrics();
        }

        private async void DisplayLyrics()
        {
            try
            {
                TextBoxForLyrics.Text = DownloadedLyrics[LyricsId];
            }
            catch
            {
                DownloadedLyrics.Add(await GetLyrics(references[LyricsId]));
                TextBoxForLyrics.Text = DownloadedLyrics[LyricsId];
            }
            finally
            {
                UpdateLyricsSelector();
            }
        }
    }
}
