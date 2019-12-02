using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
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
                default:
                    throw new ArgumentException(browser.ToString());
            }
        }
    }
}
