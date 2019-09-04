using System.IO;

namespace Seleniun.Example
{
    class Program
    {
        static void Main()
        {
            var seleniun = new Seleniun(SeleniumBrowserType.Firefox, "c:\\Temp", Directory.GetCurrentDirectory(), 10);

            seleniun
                .OpenUrl("https://www.google.com/")
                .FillByName("q", "devops")
                .ClickButtonByName("btnK")                
                .CloseBrowser();
        }
    }
}
