using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;

namespace SeleniuN
{
    public static class SeleniumBrowserFactory
    {
        public static IWebDriver Get(SeleniumBrowser browser)
        {
            switch (browser)
            {
                case SeleniumBrowser.Chrome:
                    return new ChromeDriver();
                case SeleniumBrowser.Firefox:
                    return new FirefoxDriver();
                case SeleniumBrowser.InternetExplorer:
                    return new InternetExplorerDriver();
                default:
                    throw new ArgumentException(browser.ToString());
            }
        }
    }
}
