using System;
using System.IO;
using OpenQA.Selenium;

namespace Seleniun.Example
{
    class Program
    {
        static void Main()
        {
            var seleniun = new FluentSeleniun(
                  browserType: SeleniumBrowserType.Firefox,
                  snapShotPath: "c:\\Selenium_SnapShots\\",
                  driverFolderPath: Directory.GetCurrentDirectory(),
                  maxWaitSeconds: 10);

            try
            {
                seleniun
                    .OpenUrl("https://www.google.com/")
                    .FillByName("q", "devops")
                    .ClickButtonByName("btnK123"); //wrong button name
            }
            catch (Exception ex)
            {
                seleniun.SavePrint(ex);
            }
            finally
            {
                seleniun.CloseBrowser();
            }
        }
    }
}