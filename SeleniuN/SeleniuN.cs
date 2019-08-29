using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniuN;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Seleniun
{
    public class Seleniun
    {
        private readonly IWebDriver _browser;
        private readonly WebDriverWait _wait;
        private readonly string _snapShotPath;

        public Seleniun(IWebDriver webDriver, string snapShotPath, int waitSeconds = 5)
        {
            _browser = webDriver;
            _wait = new WebDriverWait(_browser, TimeSpan.FromSeconds(waitSeconds));
            _snapShotPath = snapShotPath;
        }

        public Seleniun(SeleniumBrowser browserType, string snapShotPath, int waitSeconds = 5)
        {
            _browser = SeleniumBrowserFactory.Get(browserType);
            _wait = new WebDriverWait(_browser, TimeSpan.FromSeconds(waitSeconds));
            _snapShotPath = snapShotPath;
        }

        public IWebElement GetByName(string name)
        {
            _wait.Until(ExpectedConditions.ElementExists(By.Name(name)));
            return _browser.FindElement(By.Name(name));
        }

        public IWebElement GetById(string id)
        {
            _wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
            return _browser.FindElement(By.Id(id));
        }

        public IWebElement GetByXPath(string xpath)
        {
            _wait.Until(ExpectedConditions.ElementExists(By.XPath(xpath)));
            return _browser.FindElement(By.XPath(xpath));
        }

        public void AlertClick()
        {
            _wait.Until(AlertExists);

            _browser.SwitchTo().Alert().Accept();
        }

        public void ClickButtonByName(string name)
        {
            GetByName(name).Click();
        }

        public void ClickButtonById(string id)
        {
            GetById(id).Click();
        }

        public void ClickButtonByXPath(string xpath)
        {
            GetByXPath(xpath).Click();
        }

        public void FillByName(string name, string value)
        {
            var element = GetByName(name);
            element.Clear();
            element.SendKeys(value);
        }

        public void FillById(string id, string value)
        {
            var element = GetById(id);
            element.Clear();
            element.SendKeys(value);
        }

        public void FillByXPath(string xpath, string value)
        {
            var element = GetByXPath(xpath);
            element.Clear();
            element.SendKeys(value);
        }

        public void SelectDdlByName(string name, string value)
        {
            var element = GetByName(name);
            new SelectElement(element).SelectByText(value);
        }

        public void SelectDdlById(string id, string value)
        {
            var element = GetById(id);
            new SelectElement(element).SelectByText(value);
        }

        public void SelectDdlByXPath(string xpath, string value)
        {
            var element = GetByXPath(xpath);
            new SelectElement(element).SelectByText(value);
        }

        public void WaitByName(string name)
        {
            _wait.Until(ExpectedConditions.ElementExists(By.Name(name)));
        }

        public void WaitById(string id)
        {
            _wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
        }

        public void WaitByXPath(string xpath)
        {
            _wait.Until(ExpectedConditions.ElementExists(By.XPath(xpath)));
        }

        public void Wait(int milliseconds = 1000)
        {
            System.Threading.Thread.Sleep(milliseconds);
        }

        public bool WaitAlert()
        {
            return _wait.Until(AlertExists);
        }

        public IAlert GetAlert()
        {
            try
            {
                _wait.Until(AlertExists);

                return _browser.SwitchTo().Alert();
            }
            catch (Exception)
            {
                return null;
            }
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

        public void SavePrint(Exception erro)
        {
            Directory.CreateDirectory(_snapShotPath);

            var screnn = (ITakesScreenshot)_browser;

            Screenshot print = screnn.GetScreenshot();
            print.SaveAsFile(_snapShotPath + erro.Message.Replace(" ", "_") + ".png", ScreenshotImageFormat.Png);
        }

        public void CloseBrowser()
        {
            _browser.Quit();
        }

        public void OpenUrl(string url)
        {
            _browser.Navigate().GoToUrl(url);
        }

    }
}
