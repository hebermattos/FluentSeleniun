using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace OpenQA.Selenium
{
    public class FluentSelenium
    {
        private readonly IWebDriver _browser;
        private readonly WebDriverWait _wait;
        private readonly string _errorSnapShotPath;
        private IList<Action> _methods;

        public FluentSelenium(SeleniumBrowserType browserType, string errorSnapShotPath, string driverFolderPath, int maxWaitSeconds)
        {
            _browser = SeleniumBrowserFactory.Get(browserType, driverFolderPath);
            _wait = new WebDriverWait(_browser, TimeSpan.FromSeconds(maxWaitSeconds));
            _wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            _errorSnapShotPath = errorSnapShotPath;
            _methods = new List<Action>();
        }

        public FluentSelenium OpenUrl(string url)
        {
            _methods.Add(() =>
            {
                _browser.Navigate().GoToUrl(url);
            });

            return this;
        }

        public FluentSelenium PageTitleShouldBe(string text)
        {
            _methods.Add(() =>
            {
                if (!_browser.Title.Equals(text, StringComparison.InvariantCultureIgnoreCase))
                    throw new Exception($"Page title should be {text}");
            });

            return this;
        }

        public FluentSelenium PageTitleShouldContains(string text)
        {
            _methods.Add(() =>
            {
                if (!_browser.Title.Contains(text))
                    throw new Exception($"Page title should contains {text}");
            });

            return this;
        }

        public FluentSelenium PageShouldContains(string text)
        {
            _methods.Add(() =>
            {
                if (!_browser.PageSource.Contains(text))
                    throw new Exception($"Page should contains {text}");
            });

            return this;
        }

        public FluentSelenium ClickAlert()
        {
            _methods.Add(() =>
            {
                _wait.Until(AlertExists);
                _browser.SwitchTo().Alert().Accept();
            });

            return this;
        }

        public FluentSelenium ClickButtonByName(string name)
        {
            _methods.Add(() =>
            {
                GetByName(name).Click();
            });

            return this;
        }

        public FluentSelenium ClickButtonById(string id)
        {
            _methods.Add(() =>
            {
                GetById(id).Click();
            });

            return this;
        }

        public FluentSelenium ClickButtonByXPath(string xpath)
        {
            _methods.Add(() =>
            {
                GetByXPath(xpath).Click();
            });

            return this;
        }

        public FluentSelenium FillByName(string name, string value)
        {
            _methods.Add(() =>
            {
                var element = GetByName(name);
                SendKeys(value, element);
            });

            return this;
        }

        public FluentSelenium FillById(string id, string value)
        {
            _methods.Add(() =>
            {
                var element = GetById(id);
                SendKeys(value, element);
            });

            return this;
        }

        public FluentSelenium FillByXPath(string xpath, string value)
        {
            _methods.Add(() =>
            {
                var element = GetByXPath(xpath);
                SendKeys(value, element);
            });

            return this;
        }

        public FluentSelenium SelectDdlByName(string name, string value)
        {
            _methods.Add(() =>
            {
                var element = GetByName(name);
                new SelectElement(element).SelectByText(value);
            });

            return this;
        }

        public FluentSelenium SelectDdlById(string id, string value)
        {
            _methods.Add(() =>
            {
                var element = GetById(id);
                new SelectElement(element).SelectByText(value);
            });

            return this;
        }

        public FluentSelenium SelectDdlByXPath(string xpath, string value)
        {
            _methods.Add(() =>
           {
               var element = GetByXPath(xpath);
               new SelectElement(element).SelectByText(value);
           });

            return this;
        }

        public bool Run()
        {
            try
            {
                foreach (var method in _methods)
                    method.Invoke();
                
                return true;
            }
            catch (Exception ex)
            {
                SavePrint(ex);

                return false;
            }
            finally
            {
                CloseBrowser();
            }
        }

        #region Private

        private void SavePrint(Exception erro)
        {
            Directory.CreateDirectory(_errorSnapShotPath);

            var screnn = (ITakesScreenshot)_browser;

            Screenshot print = screnn.GetScreenshot();
            print.SaveAsFile(_errorSnapShotPath + RemoveSpecialCharacters(erro.Message.Replace(" ", "_")) + ".png", ScreenshotImageFormat.Png);
        }

        private static void SendKeys(string value, IWebElement element)
        {
            element.Clear();
            element.SendKeys(value);
        }

        private static string RemoveSpecialCharacters(string str)
        {
            var sb = new StringBuilder();

            foreach (char c in str)
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                    sb.Append(c);

            return sb.ToString();
        }

        private IWebElement GetByName(string name)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.Name(name)));
            return _browser.FindElement(By.Name(name));
        }

        private IWebElement GetById(string id)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(id)));
            return _browser.FindElement(By.Id(id));
        }

        private IWebElement GetByXPath(string xpath)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(xpath)));
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

        private void CloseBrowser()
        {
            _browser.Quit();
        }

        #endregion 
    }
}