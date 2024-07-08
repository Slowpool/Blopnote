using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blopnote
{
    public class Browser
    {
        private static Browser instance;
        public static bool Created => instance != null;
        public static Browser Instance => Created ? instance : instance = new Browser();

        private ChromeDriver driver;
        private readonly ChromeOptions options;
        private readonly ChromeDriverService service;
        private WebDriverWait wait;

        private Browser()
        {
            options = new ChromeOptions();
            options.AddArgument("--headless"); // Hide the browser window
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-gpu"); // Disable hardware acceleration.
            options.PageLoadStrategy = PageLoadStrategy.Eager;

            service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            StartNewDriver();
            driver.Manage().Window.Maximize();
        }

        public void CloseIfExists()
        {
            if (Created)
            {
                driver.Quit();
            }
        }

        public List<string> RequestForSimilarSongs(string songName)
        {
            try
            {
                OpenLyricsSearchPage(songName);
            }
            catch
            {
                StartNewDriver();
                OpenLyricsSearchPage(songName);
            }
            Task.Delay(2000);

            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> cards;
            wait.IgnoreExceptionTypes();
            try
            {
                cards = wait.Until(webDriver =>
                {
                    var result = webDriver.FindElements(By.ClassName("mini_card"));
                    return result.Count >= 1 ? result : null;
                });
            }
            catch
            {
                return new List<string>();
            }

            List<string> references = new List<string>();
            string reference;
            foreach (var card in cards)
            {
                reference = card.GetAttribute("href");
                if (!references.Contains(reference))
                {
                    references.Add(reference);
                }
            }
            return references;
        }

        private void StartNewDriver()
        {
            driver?.Dispose();
            driver = new ChromeDriver(service, options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private void OpenLyricsSearchPage(string songName)
        {
            driver.Navigate().GoToUrl("https://genius.com/search?q=" + songName);
        }

        public string[] GetTranslationByGoogle(string lyrics)
        {
            driver.Navigate().GoToUrl("https://translate.google.com/?sl=ru&tl=en&text=" + lyrics.Replace("\r\n", "%0A"));
            wait.IgnoreExceptionTypes(typeof(InvalidOperationException));
            try
            {
                IWebElement soundButton = wait.Until(webDriver => webDriver.FindElements(By.XPath("//div[@class='aJIq1d' and @data-language-code='en']")).Single());
                return soundButton.GetAttribute("data-text")
                                  .Split(new[] { "\r\n" }, StringSplitOptions.None);
            }
            catch
            {
                string[] latchArray = new string[lyrics.Length];
                for(int i = 0; i < latchArray.Length; i++)
                {
                    latchArray[i] = "Error. Translation wasn't loaded";
                }
                return latchArray;
            }
        }

        public string[,] GetYoutubeUrls(string songName)
        {
            driver.Navigate().GoToUrl("https://www.youtube.com/results?search_query=" + songName);
            var videoTitles = wait.Until(driver =>
            {
                var videos = driver.FindElements(By.Id("video-title"));
                return videos.Count() >= 5
                     ? videos.Take(5)
                     : null;
            });
            string[,] result = new string[videoTitles.Count(), 2];
            int i = 0;
            foreach (var videoTitle in videoTitles)
            {
                result[i, 0] = videoTitle.GetAttribute("title");
                result[i, 1] = videoTitle.GetAttribute("href");
                i++;
            }

            return result;
            #region latch
            //switch (new Random().Next(6))
            //{
            //    case 0:
            //        return new string[0];
            //    case 1:
            //        return new[] { "ref1" };
            //    case 2:
            //        return new[] { "ref1", "ref2" };
            //    case 3:
            //        return new[] { "ref1", "ref2", "ref3" };
            //    case 4:
            //        return new[] { "ref1", "ref2", "ref3", "ref4" };
            //    case 5:
            //        return new[] { "ref1", "ref2", "ref3", "ref4", "ref5" };
            //    default:
            //        return new string[] { "https://www.youtube.com/watch?v=e26zZ83Oh6Y", "https://www.youtube.com/watch?v=KNqRoKLZZ6M" };
            //} 
            #endregion
        }

        public void OpenUrlForUser(string Url)
        {
            try
            {
                var info = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = Url,
                    CreateNoWindow = false,
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized
                };
                System.Diagnostics.Process.Start(info);
            }
            catch (Exception e)
            {
                MessageBox.Show(caption: "Url opening error",
                                text: "Url wasn't opened correctly. Cause: " + e.Message,
                                buttons: MessageBoxButtons.OK,
                                icon: MessageBoxIcon.Error);
            }
        }

        //public void Close()
        //{
        //    driver.Close();
        //}

        public string GetLyrics(string GeniusSongUrl)
        {
            driver.Navigate().GoToUrl(GeniusSongUrl);
            Task.Delay(1000);
            IEnumerable<IWebElement> divs = driver.FindElements(By.ClassName("Lyrics__Container-sc-1ynbvzw-1"));
            return divs.Aggregate(string.Empty, (lyrics, div) => lyrics + div.Text);
            //// latch
            //return "haha";
        }
    }
}
