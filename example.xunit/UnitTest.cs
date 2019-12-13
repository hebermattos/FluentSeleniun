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
                  snapShotPath: "c:\\Selenium_SnapShots\\",
                  driverFolderPath: Directory.GetCurrentDirectory(),
                  maxWaitSeconds: 30);
        }

        [Fact]
        public void Test1()
        {
            var testResult = _seleniun
                 .OpenUrl("https://www.google.com/")
                 .PageTitleShouldBe("Google")
                 .FillByName("q", "devops")
                 .ClickButtonByName("btnK")
                 .PageTitleShouldContains("devops")
                 .Run();

            Assert.True(testResult);
        }

        [Fact]
        public void Test2()
        {
            var testResult = _seleniun
                 .OpenUrl("https://www.google.com/")
                 .PageTitleShouldBe("Google")
                 .FillByName("q", "github")
                 .ClickButtonByName("btnK")
                 .PageTitleShouldContains("github")
                 .Run();

            Assert.True(testResult);
        }
    }
}
