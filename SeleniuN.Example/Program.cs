using System;
using System.IO;

namespace Seleniun.Example
{
    class Program
    {
        static void Main()
        {
            var seleniun = new Seleniun(
                  browserType: SeleniumBrowserType.Firefox,
                  snapShotPath: "c:\\Selenium_SnapShots\\",
                  driverFolderPath: Directory.GetCurrentDirectory(),
                  maxWaitSeconds: 10);

            try
            {
                seleniun
                    .OpenUrl("https://www.google.com/")
                    .FillByName("q", "devops")
                    .ClickButtonByName("btnK123");
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