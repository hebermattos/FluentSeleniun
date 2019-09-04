using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Seleniun;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Seleniun
{
    public class Seleniun
    {
        private readonly IWebDriver _browser;
        private readonly WebDriverWait _wait;
        private readonly string _snapShotPath;

        private Seleniun(string snapShotPath, int waitSeconds = 5)
        {            
            _snapShotPath = snapShotPath;
        }

        public Seleniun(IWebDriver webDriver, string snapShotPath, int waitSeconds = 5): this(snapShotPath)
        {
            _browser = webDriver;
            _wait = new WebDriverWait(_browser, TimeSpan.FromSeconds(waitSeconds));
        }

        public Seleniun(SeleniumBrowserType browserType, string snapShotPath, int waitSeconds = 5) : this(snapShotPath)
        {
            _browser = SeleniumBrowserFactory.Get(browserType);
            _wait = new WebDriverWait(_browser, TimeSpan.FromSeconds(waitSeconds));
        }

        public Seleniun OpenUrl(string url)
        {
            _browser.Navigate().GoToUrl(url);

            return this;
        }

        public Seleniun ClickAlert()
        {
            _wait.Until(AlertExists);
            _browser.SwitchTo().Alert().Accept();

            return this;
        }

        public Seleniun ClickButtonByName(string name)
        {
            GetByName(name).Click();

            return this;
        }

        public Seleniun ClickButtonById(string id)
        {
            GetById(id).Click();

            return this;
        }

        public Seleniun ClickButtonByXPath(string xpath)
        {
            GetByXPath(xpath).Click();

            return this;
        }

        public Seleniun FillByName(string name, string value)
        {
            var element = GetByName(name);
            element.Clear();
            element.SendKeys(value);

            return this;
        }

        public Seleniun FillById(string id, string value)
        {
            var element = GetById(id);
            element.Clear();
            element.SendKeys(value);

            return this;
        }

        public Seleniun FillByXPath(string xpath, string value)
        {
            var element = GetByXPath(xpath);
            element.Clear();
            element.SendKeys(value);

            return this;
        }

        public Seleniun SelectDdlByName(string name, string value)
        {
            var element = GetByName(name);
            new SelectElement(element).SelectByText(value);

            return this;
        }

        public Seleniun SelectDdlById(string id, string value)
        {
            var element = GetById(id);
            new SelectElement(element).SelectByText(value);

            return this;
        }

        public Seleniun SelectDdlByXPath(string xpath, string value)
        {
            var element = GetByXPath(xpath);
            new SelectElement(element).SelectByText(value);

            return this;
        }

        public Seleniun CloseBrowser()
        {
            _browser.Quit();

            return this;
        }

        public void SavePrint(Exception erro)
        {
            Directory.CreateDirectory(_snapShotPath);

            var screnn = (ITakesScreenshot)_browser;

            Screenshot print = screnn.GetScreenshot();
            print.SaveAsFile(_snapShotPath + erro.Message.Replace(" ", "_") + ".png", ScreenshotImageFormat.Png);
        }

        private IWebElement GetByName(string name)
        {
            _wait.Until(ExpectedConditions.ElementExists(By.Name(name)));
            return _browser.FindElement(By.Name(name));
        }

        private IWebElement GetById(string id)
        {
            _wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
            return _browser.FindElement(By.Id(id));
        }

        private IWebElement GetByXPath(string xpath)
        {
            _wait.Until(ExpectedConditions.ElementExists(By.XPath(xpath)));
            return _browser.FindElement(By.XPath(xpath));
        }

        private bool AlertExists(IWebDriver browser)
        {
            try
            {
                return browser.SwitchTo().Alert() != null;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
    }
}
