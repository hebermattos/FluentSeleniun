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
                  browserType: SeleniumBrowserType.Chrome,
                  snapShotPath: "c:\\Selenium_SnapShots\\",
                  driverFolderPath: Directory.GetCurrentDirectory(),
                  maxWaitSeconds: 3);

            seleniun
                .OpenUrl("https://www.google.com/")
                .PageTitleShoulBe("Google")
                .FillByName("q", "devops")
                .ClickButtonByName("btnK")
                .PageTitleShoulContains("devops")
                .Run();
        }
    }
}