using System.IO;
using OpenQA.Selenium;
using Xunit;

namespace example.test
{

    public class UnitTest1
    {
        private FluentSelenium _seleniun;

        public UnitTest1()
        {
            _seleniun = new FluentSelenium(
                  browserType: SeleniumBrowserType.Chrome,
                  snapShotPath: "c:\\Selenium_SnapShots\\",
                  driverFolderPath: Directory.GetCurrentDirectory(),
                  maxWaitSeconds: 3);
        }

        [Fact]
        public void Test1()
        {
            _seleniun
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
            _seleniun
                .OpenUrl("https://www.google.com/")
                .PageTitleShouldBe("Google")
                .FillByName("q", "github")
                .ClickButtonByName("btnK")
                .PageTitleShouldContains("github")
                .Run();
        }
    }
}
