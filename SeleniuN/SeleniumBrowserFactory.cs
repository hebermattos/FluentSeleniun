using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.IO;

namespace Seleniun
{
    public static class SeleniumBrowserFactory
    {
        public static IWebDriver Get(SeleniumBrowserType browser)
        {
            var directory = Directory.GetCurrentDirectory();

            switch (browser)
            {
                case SeleniumBrowserType.Chrome:
                    return new ChromeDriver(directory);
                case SeleniumBrowserType.Firefox:
                    return new FirefoxDriver(directory);
                case SeleniumBrowserType.InternetExplorer:
                    return new InternetExplorerDriver(directory);
                default:
                    throw new ArgumentException(browser.ToString());
            }
        }
    }
}
