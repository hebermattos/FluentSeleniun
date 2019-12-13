# seleniun
simple selenium wrapper

            var seleniun = new FluentSeleniun(
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
