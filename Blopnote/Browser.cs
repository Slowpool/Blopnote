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
            options.AddArgument("--headless"); // Hide the browser window
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-gpu"); // Disable hardware acceleration.
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

            driver.Navigate().GoToUrl("https://genius.com/search?q=" + songName);
            await Task.Delay(2000);

            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> cards;
            wait.IgnoreExceptionTypes();
            try
            {
                cards = wait.Until(webDriver =>
                {
                    var result = webDriver.FindElements(By.ClassName("mini_card"));
                    return result.Count >= 1
                         ? result
                         : null;
                });
            }
            catch
            {
                Cursor.Current = Cursors.Default;
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
            Cursor.Current = Cursors.Default;
            return references;
            // latch
            return null;
        }

        internal static string[] GetTranslationByGoogle(string lyrics)
        {
            Cursor.Current = Cursors.WaitCursor;
            driver.Navigate().GoToUrl("https://translate.google.com/?sl=ru&tl=en&text=" + lyrics.Replace("\r\n", "%0A"));
            wait.IgnoreExceptionTypes(typeof(InvalidOperationException));
            try
            {
                var soundButton =
                    wait.Until(webDriver =>
                    {
                        return webDriver.FindElements(By.ClassName("aJIq1d"))
                                        .Where(element => element.GetAttribute("data-language-code") == "en")
                                        .Single();
                    });
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
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        internal static string[,] GetYoutubeUrls(string songName)
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
            foreach (var videoTitle in videoTitles)
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

        internal static void OpenUrl(string Url)
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
            //catch (InvalidOperationException e)
            catch (Exception e)
            {
                MessageBox.Show(caption: "Url opening error",
                                text: "Url wasn't opened correctly. Cause: " + e.Message,
                                buttons: MessageBoxButtons.OK,
                                icon: MessageBoxIcon.Error);
            }
        }
    }
}
