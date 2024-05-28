using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blopnote
{
    internal static class Browser
    {
        public static readonly ChromeDriver driver;

        static Browser()
        {
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


    }
}
