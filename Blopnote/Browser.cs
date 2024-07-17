using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blopnote
{
    public class Browser : ISongInfoSupplier
    {
        private readonly ILogger<Browser> Logger = BlopnoteLogger.CreateLogger<Browser>();

        private static Browser instance;
        private static bool Created => instance != null;
        public static Browser Instance => Created ? instance : instance = new Browser();

#error I thought about several webdrivers, but after all decided it's a bad idea.
        private readonly ChromeOptions options;
        //private readonly FirefoxOptions options;
        private readonly ChromeDriverService service;
        //private readonly FirefoxDriverService service;
        private IWebDriver driver;
        private WebDriverWait wait;

        private Browser()
        {
            Logger.LogInformation("Constructing browser");

            options = new ChromeOptions();
            //options = new FirefoxOptions();
            options.AddArgument("--headless"); // Hide the browser window
            options.AddArgument("--start-maximize");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-gpu"); // Disable hardware acceleration.
            options.PageLoadStrategy = PageLoadStrategy.Eager;

            service = ChromeDriverService.CreateDefaultService();
            //service = FirefoxDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            TryCreateDriver();
        }

        ~Browser()
        {
            Close();
        }

        public List<string> FindSimilarSongs(string songName)
        {
            Logger.LogInformation("Looking for songs with name {name}", songName);
            driver.Navigate().GoToUrl("https://genius.com/search?q=" + songName);
            Wait(2000);

            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> cards;
            try
            {
                cards = wait.Until(webDriver =>
                {
                    return webDriver.FindElements(By.ClassName("mini_card"));
                });
            }
            catch (WebDriverTimeoutException exception)
            {
                Logger.LogError(exception, "Songs weren't found");
                throw;
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
            Logger.LogInformation("References of found songs: {refs}", references);
            return references;
        }

        public string[] GetTranslationByGoogle(string lyrics)
        {
            driver.Navigate().GoToUrl("https://translate.google.com/?sl=ru&tl=en&text=" + lyrics.Replace("\r\n", "%0A"));
            wait.IgnoreExceptionTypes(typeof(InvalidOperationException));
            try
            {
                IWebElement soundButton = wait.Until(webDriver => webDriver.FindElement(By.XPath("//div[@class='aJIq1d' and @data-language-code='en']")));
                return soundButton.GetAttribute("data-text")
                                  .Split(new[] { "\r\n" }, StringSplitOptions.None);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Failed to find translation by google");
                throw;
            }
        }

        public string[,] GetYoutubeReferences(string songName)
        {
            string url = "https://www.youtube.com/results?search_query=" + songName;
            try
            {
                driver.Navigate().GoToUrl(url);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Failed to follow the link: {link}", url);
                throw;
            }

            IEnumerable<IWebElement> videoTitles;
            try
            {
                videoTitles = wait.Until(driver =>
                {
                    var videos = driver.FindElements(By.Id("video-title"));
                    return videos.Count() >= 5
                         ? videos.Take(5)
                         : null;
                });
            }
            // Does it look redundant, doesn't it?
            catch (WebDriverTimeoutException exception)
            {
                Logger.LogError(exception, "Failed to find videos");
                throw;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Failed to find videos");
                throw;
            }
            
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

        public string GetLyrics(string SongUrlOnGenius)
        {
            try
            {
                driver.Navigate().GoToUrl(SongUrlOnGenius);
                Wait(1000);
                IEnumerable<IWebElement> divsWithLyrics = driver.FindElements(By.ClassName("Lyrics__Container-sc-1ynbvzw-1"));
                return divsWithLyrics.Aggregate(string.Empty, (lyrics, div) => lyrics + div.Text);
            }
            catch
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Strictrly speaking, it doesn't matter where this method is. Just here it's the most suitable from point of view of logic belonging (opens new window in browser, in spite of doing that independently of this browser.)
        /// </summary>
        /// <param name="Url"></param>
        public static void OpenUrlForUser(string Url)
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
                MessageShower.Show(BlopnoteMessageTypes.UrlOpeningError, e);
            }
        }

        private void Wait(int milliseconds)
        {
            System.Threading.Thread.Sleep(milliseconds);
        }

        public void Reconnect()
        {
            Close();
            TryCreateDriver();
        }

        private void Close()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                Logger.LogInformation("WebDriver was closed");
            }
            else
            {
                Logger.LogWarning("Browser got called Close() method, but WebDriver was null");
            }
        }

        private void TryCreateDriver()
        {
            try
            {
                driver = new ChromeDriver(service, options);
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                Logger.LogInformation("WebDriver successfully created");
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Failed to create new WebDriver");
                MessageShower.Show(BlopnoteMessageTypes.BrowserError, exception);
                //throw new BrowserCreatingException(exception.Message);
            }
        }
    }

    // expand it with more exceptions
    public class BrowserCreatingException : Exception
    {
        public BrowserCreatingException() : base("Failed to open browser.") { }
        public BrowserCreatingException(string @string) : base(@string) { }
    }
}
