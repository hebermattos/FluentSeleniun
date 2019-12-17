using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using System;

namespace OpenQA.Selenium
{
    public static class SeleniumBrowserFactory
    {
        public static IWebDriver Get(SeleniumBrowserType browser, string driverFolderPath)
        {
            switch (browser)
            {
                case SeleniumBrowserType.Chrome:
                    return new ChromeDriver(driverFolderPath);
                case SeleniumBrowserType.Firefox:
                    return new FirefoxDriver(driverFolderPath);
                case SeleniumBrowserType.InternetExplorer:
                    return new InternetExplorerDriver(driverFolderPath);
                case SeleniumBrowserType.Opera:
                    return new OperaDriver(driverFolderPath);
                case SeleniumBrowserType.Safari:
                    return new SafariDriver(driverFolderPath);
                default:
                    throw new ArgumentException(browser.ToString());
            }
        }
    }
}
