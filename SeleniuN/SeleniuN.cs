using System;
using System.Drawing.Imaging;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace RedeHost.Comando.UI.Web.Teste.Auxiliares
{
    public class SeleniuN
    {
        private readonly IWebDriver _browser;
        private readonly WebDriverWait _wait;
        private readonly string _snapShotPath;

        public SeleniuN(IWebDriver webDriver, string snapShotPath, int waitSeconds = 5)
        {
            _browser = webDriver;
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

        public void PreencherCampoPorNome(string nomeelement, string valorCampo)
        {
            var element = GetByName(nomeelement);
            element.Clear();
            element.SendKeys(valorCampo);
        }

        public void FillById(string id, string value)
        {
            var element = GetById(id);
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

        public void WaitByName(string name)
        {
            _wait.Until(ExpectedConditions.ElementExists(By.Name(name)));
        }

        public void WaitById(string id)
        {
            _wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
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

        public bool AlertExists(IWebDriver browser)
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

        public void SalvarPrint(Exception erro)
        {
            Directory.CreateDirectory(_snapShotPath);

            var tela = (ITakesScreenshot)_browser;

            Screenshot print = tela.GetScreenshot();
            print.SaveAsFile(_snapShotPath + erro.Message.Replace(" ", "_") + ".png", ImageFormat.Png);
        }

        public void Close()
        {
            _browser.Quit();
        }


        public void OpenUrl(string url)
        {
            _browser.Navigate().GoToUrl(url);
        }

    }
}
