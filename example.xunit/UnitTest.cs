using System.IO;
using OpenQA.Selenium;
using Xunit;

namespace example.test
{

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var seleniun = new FluentSelenium(
                  browserType: SeleniumBrowserType.Chrome,
                  snapShotPath: "c:\\Selenium_SnapShots\\",
                  driverFolderPath: Directory.GetCurrentDirectory(),
                  maxWaitSeconds: 3);

            seleniun
                .OpenUrl("https://www.google.com/")
                .PageTitleShouldBe("Google")
                .FillByName("q", "devops")
                .ClickButtonByName("btnK")
                .PageTitleShouldContains("devops")
                .Run();
        }

        [Fact]
        public void Test2()
        {
            var seleniun = new FluentSelenium(
                  browserType: SeleniumBrowserType.Chrome,
                  snapShotPath: "c:\\Selenium_SnapShots\\",
                  driverFolderPath: Directory.GetCurrentDirectory(),
                  maxWaitSeconds: 3);

            seleniun
                .OpenUrl("https://www.google.com/")
                .PageTitleShouldBe("Google")
                .FillByName("q", "github")
                .ClickButtonByName("btnK")
                .PageTitleShouldContains("github")
                .Run();
        }
    }
}
