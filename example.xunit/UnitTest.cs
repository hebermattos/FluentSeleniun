using System.IO;
using OpenQA.Selenium;
using Xunit;

namespace example.xunit
{
    public class UnitTest
    {
        private FluentSelenium _seleniun;

        public UnitTest()
        {
            _seleniun = new FluentSelenium(
                  browserType: SeleniumBrowserType.Chrome,
                  errorSnapShotPath: Directory.GetCurrentDirectory() + "\\Selenium_SnapShots\\",
                  driverFolderPath: Directory.GetCurrentDirectory(),
                  maxWaitSeconds: 3);
        }

        [Fact]
        public void google_search()
        {
            _seleniun
                 .OpenUrl("https://www.google.com/")
                 .PageTitleShouldBe("Google")
                 .FillByName("q", "devops")
                 .ClickButtonByName("btnK")
                 .PageTitleShouldContains("devops")
                 .Run();
        }  
    }
}