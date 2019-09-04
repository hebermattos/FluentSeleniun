using System;

namespace Seleniun.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var seleniun = new Seleniun(SeleniumBrowserType.Firefox,"c:\\Temp",10);

            seleniun
                .OpenUrl("https://www.google.com/")
                .FillByName("q", "devops")
                .ClickButtonByName("btnK");
        }
    }
}
