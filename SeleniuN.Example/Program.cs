using System;
using System.IO;

namespace Seleniun.Example
{
    class Program
    {
        static void Main()
        {
            var seleniun = new Seleniun(SeleniumBrowserType.Firefox, "c:\\Temp\\", Directory.GetCurrentDirectory(), 10);

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