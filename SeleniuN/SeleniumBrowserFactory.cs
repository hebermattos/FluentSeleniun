using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;

namespace SeleniuN
{
    public static class SeleniumBrowserFactory
    {
        public static IWebDriver Get(SeleniumBrowserType browser)
        {
            switch (browser)
            {
                case SeleniumBrowserType.Chrome:
                    return new ChromeDriver();
                case SeleniumBrowserType.Firefox:
                    return new FirefoxDriver();
                case SeleniumBrowserType.InternetExplorer:
                    return new InternetExplorerDriver();
                default:
                    throw new ArgumentException(browser.ToString());
            }
        }
    }
}
