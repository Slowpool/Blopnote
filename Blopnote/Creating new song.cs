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

        internal FileNameAndLyricsInputWindow()
        {
            InitializeComponent();
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

        private void buttonSearchOnGenius_Click(object sender, EventArgs e)
        {
            if (TextBoxForAuthor.Text.Length != 0 && TextBoxForSong.Text.Length != 0)
            {
                string songName = TextBoxForAuthor.Text + " " + TextBoxForSong.Text;
                var references = RequestForSimilarSongs(songName);
                if (references.Count == 0)
                {
                    labelSearchResult.Text = "Song wasn't found";
                }
                else
                {

                    labelSearchResult.Text = "Current lyrics belong the song: " + "songNameFromRequest";
                }
            }
        }

        private void FileNameAndLyricsInputWindow_Load(object sender, EventArgs e)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--detach");
            options.PageLoadStrategy = PageLoadStrategy.Eager;
            driver = new ChromeDriver(options);
        }

        private List<string> RequestForSimilarSongs(string songName)
        {
            driver.Navigate().GoToUrl("http://Genius.com");
            var query = driver.FindElement(By.Name("q"));
            query.Clear();
            query.SendKeys(songName);
            query.SendKeys(SeleniumKeys.Return);

            var cards = driver.FindElements(By.ClassName("mini_card"))
                              .Skip(1)
                              .Take(5);
            List<string> references = new List<string>();
            string reference;
            foreach (var card in cards)
            {
                reference = card.GetAttribute("href");
                Debug.WriteLine(reference);
                references.Add(reference);
            }
#error it doesn't always work
            return references;
        }

        internal string GetLyrics(string GeniusSongURL)
        {
            driver.Navigate().GoToUrl(GeniusSongURL);
            var divs = driver.FindElements(By.ClassName("Lyrics__Container-sc-1ynbvzw-1 kUgSbL"));
            string lyrics = string.Empty;
            foreach (var div in divs)
            {
                lyrics += GetPartOfLyrics(div);
            }
            return lyrics;
        }

        internal string GetPartOfLyrics(IWebElement div)
        {
            return string.Empty;
        }
    }
}
