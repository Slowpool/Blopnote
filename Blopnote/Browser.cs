using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeleniumKeys = OpenQA.Selenium.Keys;

namespace Blopnote
{
    internal static class Browser
    {
        internal static readonly ChromeDriver driver;
        private static readonly WebDriverWait wait;

        internal static bool IsOpened { get; set; } = false;

        /// <summary>
        /// Use it for browser driver initialiation
        /// </summary>
        internal static void Latch()
        {
            
        }

        static Browser()
        {
            IsOpened = true;
            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("--headless"); // Hide the browser window
            options.AddArgument("--disable-extensions");
            //options.AddArgument("--disable-gpu"); // Disable hardware acceleration.
            options.PageLoadStrategy = PageLoadStrategy.Eager;

            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            driver = new ChromeDriver(service, options);
            driver.Manage().Window.Maximize();
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        internal static async Task<List<string>> RequestForSimilarSongs(string songName)
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
            // latch
            return null;
        }

        internal static string[] GetTranslationByGoogle(string lyrics)
        {
            #region trash
            // yandex and reverso attempts
            ////driver.Navigate().GoToUrl("https://translate.yandex.ru/&source_lang=ru&target_lang=en&text=" + Lyrics); // yandex
            //driver.Navigate().GoToUrl("https://www.reverso.net/#sl=rus&tl=eng&text=" + Lyrics.Replace("\r\n", "%250A"));
            ////var inputBox = driver.FindElement(By.XPath("//textarea[contains(@class,'no-border no-outline no-outline-hover no-outline-focus textarea__textarea ng-valid ng-dirty ng-touched')]"));
            ////await Task.Delay(500);
            ////inputBox.SendKeys(Lyrics);
            //await Task.Delay(10000);
            //string translatedLyrics = driver.FindElement(By.ClassName("sentence-wrapper_without-hover")).Text;
            #endregion
            Cursor.Current = Cursors.WaitCursor;
            driver.Navigate().GoToUrl("https://translate.google.com/?sl=ru&tl=en&text=" + lyrics.Replace("\r\n", "%0A"));
#warning needs reworking
            wait.IgnoreExceptionTypes(typeof(InvalidOperationException));
            var soundButtons =
                wait.Until(webDriver =>
                {
                    var buttons = webDriver.FindElements(By.ClassName("aJIq1d"))
                                           .Where(element => element.GetAttribute("data-language-code") == "en");
                    return buttons.Count() == 1
                         ? buttons
                         : null;
                });
            Cursor.Current = Cursors.Default;
            return soundButtons.Single()
                               .GetAttribute("data-text")
                               .Split(new[] { "\r\n" }, StringSplitOptions.None);
        }

        internal async static Task<string[,]> GetYoutubeURLs(string songName)
        {
            Cursor.Current = Cursors.WaitCursor;

            driver.Navigate().GoToUrl("https://www.youtube.com/results?search_query=" + songName);
            #region Delete
            //var search = wait.Until(webDriver => webDriver.FindElement(By.Id("search-input")));
            //await Task.Delay(5000);
            //search.Click();
            //search.SendKeys(songName);
            //search.SendKeys(SeleniumKeys.Enter); 
            #endregion
            var videoTitles = wait.Until(driver =>
            {
                var videos = driver.FindElements(By.Id("video-title"));
                return videos.Count() >= 5
                     ? videos.Take(5)
                     : null;
            });
            string[,] result = new string[videoTitles.Count(), 2];
            int i = 0;
            foreach(var videoTitle in videoTitles)
            {
                result[i, 0] = videoTitle.GetAttribute("title");
                result[i, 1] = videoTitle.GetAttribute("href");

                i++;
            }

            Cursor.Current = Cursors.Default;
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

        internal static void OpenURL(string URL)
        {
            var info = new System.Diagnostics.ProcessStartInfo
            {
                FileName = URL,
                CreateNoWindow = false
            };
            System.Diagnostics.Process.Start(info);
        }
    }
}
