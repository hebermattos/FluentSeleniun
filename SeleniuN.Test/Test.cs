using System;
using System.IO;
using Seleniun;
using Xunit;

namespace SeleniuN.Test
{
    public class Test : IDisposable
    {
        private Seleniun.Seleniun seleniun;

        [Fact]
        public void Test_1()
        {
            try
            {
                seleniun = new Seleniun.Seleniun(
                    browserType: SeleniumBrowserType.Firefox,
                    snapShotPath: "c:\\Selenium_SnapShots\\",
                    driverFolderPath: Directory.GetCurrentDirectory(),
                    maxWaitSeconds: 2);

                seleniun
                    .OpenUrl("https://www.google.com/")
                    .FillByName("q", "devops")
                    .ClickButtonByName("btnK123"); //wrong button name
            }
            catch (Exception ex)
            {
                seleniun.SavePrint(ex);
                throw;
            }
        }

        [Fact]
        public void Test_2()
        {
            try
            {
                seleniun = new Seleniun.Seleniun(
                                 browserType: SeleniumBrowserType.Firefox,
                                 snapShotPath: "c:\\Selenium_SnapShots\\",
                                 driverFolderPath: Directory.GetCurrentDirectory(),
                                 maxWaitSeconds: 2);

                seleniun
                    .OpenUrl("https://www.google.com/")
                    .FillByName("q", "devops")
                    .ClickButtonByName("btnK"); //correct button name
            }
            catch (Exception ex)
            {
                seleniun.SavePrint(ex);
                throw;
            }
        }

        public void Dispose()
        {
            seleniun.CloseBrowser();
        }

    }
}
