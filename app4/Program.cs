using CG.Web.MegaApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static SeleniumLib.simple;
using KEYS = OpenQA.Selenium.Keys;

namespace app4
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //MegaApiClient apiClient = new MegaApiClient();
            //IWebDriver browser = Launch(true);
            //browser.Url = "https://mega.nz/login";
            //WhaitLoad(browser, "id", "login-name2", "cedricgonfaron@hotmail.fr" + KEYS.Tab + "gounfaroun" + KEYS.Enter);

        }
    }
}
