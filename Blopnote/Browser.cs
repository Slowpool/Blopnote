using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeleniumKeys = OpenQA.Selenium.Keys;

namespace Blopnote
{
    internal static class Browser
    {
        internal static readonly ChromeDriver driver;
        internal static bool IsOpened { get; set; } = false;

        internal static void Initialize()
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

        internal static async Task<string[]> UpdateTranslationByGoogle(string lyrics)
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
            await Task.Delay(6000);//(Lyrics.Length > 3000 ? Lyrics.Length : 3000);
            string translatedLyrics = driver.FindElements(By.ClassName("aJIq1d"))
                                            .Where(element => element.GetAttribute("data-language-code") == "en")
                                            .Single()
                                            .GetAttribute("data-text");
            Cursor.Current = Cursors.Default;
            return translatedLyrics.Split(new[] { "\r\n" }, StringSplitOptions.None);

        }
    }
}
