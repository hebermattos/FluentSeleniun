using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace OpenQA.Selenium
{
    public class FluentSeleniun
    {
        private readonly IWebDriver _browser;
        private readonly WebDriverWait _wait;
        private readonly string _snapShotPath;
        private List<Action> _methods;

        public FluentSeleniun(SeleniumBrowserType browserType, string snapShotPath, string driverFolderPath, int maxWaitSeconds)
        {
            _browser = SeleniumBrowserFactory.Get(browserType, driverFolderPath);
            _wait = new WebDriverWait(_browser, TimeSpan.FromSeconds(maxWaitSeconds));
            _wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            _snapShotPath = snapShotPath;
            _methods = new List<Action>();
        }

        public FluentSeleniun OpenUrl(string url)
        {
            _methods.Add(() =>
            {
                _browser.Navigate().GoToUrl(url);
            });

            return this;
        }

        public FluentSeleniun PageTitleShoulBe(string text, StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase)
        {
            _methods.Add(() =>
            {
                if (!_browser.Title.Equals(text, stringComparison))
                    throw new Exception($"Page title should be {text}");
            });

            return this;
        }

        public FluentSeleniun PageTitleShoulContains(string text)
        {
            _methods.Add(() =>
            {
                if (!_browser.Title.Contains(text))
                    throw new Exception($"Page title should contains {text}");
            });

            return this;
        }

        public FluentSeleniun PageShouldContains(string text)
        {
            _methods.Add(() =>
            {
                if (!_browser.PageSource.Contains(text))
                    throw new Exception($"Page should contains {text}");
            });

            return this;
        }

        public FluentSeleniun ClickAlert()
        {
            _methods.Add(() =>
            {
                _wait.Until(AlertExists);
                _browser.SwitchTo().Alert().Accept();
            });

            return this;
        }

        public FluentSeleniun ClickButtonByName(string name)
        {
            _methods.Add(() =>
            {
                GetByName(name).Click();
            });

            return this;
        }

        public FluentSeleniun ClickButtonById(string id)
        {
            _methods.Add(() =>
            {
                GetById(id).Click();
            });

            return this;
        }

        public FluentSeleniun ClickButtonByXPath(string xpath)
        {
            _methods.Add(() =>
            {
                GetByXPath(xpath).Click();
            });

            return this;
        }

        public FluentSeleniun FillByName(string name, string value)
        {
            _methods.Add(() =>
            {
                var element = GetByName(name);
                element.Clear();
                element.SendKeys(value);
            });

            return this;
        }

        public FluentSeleniun FillById(string id, string value)
        {
            _methods.Add(() =>
            {
                var element = GetById(id);
                element.Clear();
                element.SendKeys(value);
            });

            return this;
        }

        public FluentSeleniun FillByXPath(string xpath, string value)
        {
            _methods.Add(() =>
            {
                var element = GetByXPath(xpath);
                element.Clear();
                element.SendKeys(value);
            });

            return this;
        }

        public FluentSeleniun SelectDdlByName(string name, string value)
        {
            _methods.Add(() =>
            {
                var element = GetByName(name);
                new SelectElement(element).SelectByText(value);
            });

            return this;
        }

        public FluentSeleniun SelectDdlById(string id, string value)
        {
            _methods.Add(() =>
            {
                var element = GetById(id);
                new SelectElement(element).SelectByText(value);
            });

            return this;
        }

        public FluentSeleniun SelectDdlByXPath(string xpath, string value)
        {
            _methods.Add(() =>
           {
               var element = GetByXPath(xpath);
               new SelectElement(element).SelectByText(value);
           });

            return this;
        }


        public void Run()
        {
            try
            {
                foreach (var method in _methods)
                {
                    method.Invoke();
                }
            }
            catch (Exception ex)
            {
                SavePrint(ex);
            }
            finally
            {
                CloseBrowser();
            }
        }

        private void SavePrint(Exception erro)
        {
            Directory.CreateDirectory(_snapShotPath);

            var screnn = (ITakesScreenshot)_browser;

            Screenshot print = screnn.GetScreenshot();
            print.SaveAsFile(_snapShotPath + RemoveSpecialCharacters(erro.Message.Replace(" ", "_")) + ".png", ScreenshotImageFormat.Png);
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

        private void CloseBrowser()
        {
            _browser.Quit();
        }

    }
}
