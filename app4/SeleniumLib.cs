using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Threading;

namespace SeleniumLib {
    class simple {
        private static bool DEBUG = true; // enable | disable debug mode
        public static String DFN = "Debug.txt", // debug file name
            RFN = "Result.txt"; // result file name

        /// <summary>
        /// Find IWebElement for some actions.
        /// </summary>
        /// <param name="browser">The IWebDriver element.</param>
        /// <param name="method">The element find method like : "id", "class", "name", "xpath".</param>
        /// <param name="text">The find method pointer.</param>
        /// <param name="make">The action name like : "click", "get", "status", "any text".</param>
        /// <param name="pos">The IWebElement position in IWebElements.</param>
        public static String GetElement(IWebDriver browser, String method, String text, String make, int pos = 0) {
            IWebElement element = null;
            try {
                if (method == "id")
                    element = browser.FindElements(By.Id(text))[pos];
                else if (method == "class")
                    element = browser.FindElements(By.ClassName(text))[pos];
                else if (method == "name")
                    element = browser.FindElements(By.Name(text))[pos];
                else if (method == "xpath")
                    element = browser.FindElements(By.XPath(text))[pos];
            }
            catch (Exception x) { }
            try {
                if (make == "click") {
                    element.Click();
                    return "true";
                }
                else if (make == "get") {
                    Thread.Sleep(50);
                    return element.Text;
                }
                else if (make == "clear")
                    element.Clear();
                else if (make == "status")
                    return "true";
                else if (make != "") {
                    element.SendKeys(make);
                    return "true";
                }
            }
            catch (Exception x) {
                DebugWrite(x.Message);
            }
            return "false";
        }

        /// <summary>
        /// Whait while IWebElement load for some actions.
        /// </summary>
        /// <param name="browser">The IWebDriver element.</param>
        /// <param name="method">The element find method like : "id", "class", "name", "xpath".</param>
        /// <param name="text">The find method pointer.</param>
        /// <param name="make">The action name like : "click", "get", "status", "any text".</param>
        /// <param name="pos">The IWebElement position in IWebElements.</param>
        /// <param name="limit">The limit of whait iterations on WhaitLoad.</param>
        public static String WhaitLoad(IWebDriver browser, String method, String text, String make, int pos = 0, int limit = 700) {
            int it = 0;
            IWebElement element = null;
            while (true) {
                try {
                    if (it >= limit) {
                        DebugWrite("Time overload");
                        return "false";
                    }
                    else if (method == "id")
                        element = browser.FindElements(By.Id(text))[pos];
                    else if (method == "class")
                        element = browser.FindElements(By.ClassName(text))[pos];
                    else if (method == "name")
                        element = browser.FindElements(By.Name(text))[pos];
                    else if (method == "xpath")
                        element = browser.FindElements(By.XPath(text))[pos];
                    break;
                }
                catch (Exception x) {
                    it++;
                }
            }
            try {
                if (make == "click") {
                    element.Click();
                    return "true";
                }
                else if (make == "get") {
                    Thread.Sleep(50);
                    return element.Text;
                }
                else if (make == "clear")
                    element.Clear();
                else if (make == "status")
                    return "true";
                else if (make != "") {
                    element.SendKeys(make);
                    return "true";
                }
            }
            catch (Exception x) {
                DebugWrite(x.Message);
            }
            return "false";
        }


        public static int GetLength(IWebDriver browser, String method, String text) {
            try {
                if (method == "id")
                    return browser.FindElements(By.Id(text)).Count;
                else if (method == "class")
                    return browser.FindElements(By.ClassName(text)).Count;
                else if (method == "name")
                    return browser.FindElements(By.Name(text)).Count;
                else if (method == "xpath")
                    return browser.FindElements(By.XPath(text)).Count;
            }
            catch (Exception x) {
                DebugWrite(x.Message);
            }
            return 0;
        }

        /// <summary>
        /// Write log message to debug file.
        /// </summary>
        public static void DebugWrite(String text) {
            try {
                if (DEBUG)
                    File.AppendAllText(DFN, text + Environment.NewLine);
            }
            catch { }
        }

        /// <summary>
        /// Launch browser with parameter of visible.
        /// </summary>
        public static IWebDriver Launch(bool visible) {
            /* Hide CMD */
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            /* Hide Browser */
            ChromeOptions option = new ChromeOptions();
            option.AddArgument("--headless");
            /* Start browser with 10 timeout */
            IWebDriver Chrome;
            if (visible)
                Chrome = new ChromeDriver();
            else
                Chrome = new ChromeDriver(chromeDriverService, option);
            Chrome.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            return Chrome;
        }

        /// <summary>
        /// Close all copies of Browser.
        /// </summary>
        public static void BrowserClose(IWebDriver browser) {
            try {
                browser.Close();
                browser.Quit();
                //System.Diagnostics.Process.GetProcessesByName("chromedriver")[0].Kill();
                //System.Diagnostics.Process.GetProcessesByName("chrome")[0].Kill();
            }
            catch { }
        }
    }
}

/* Examples 
  GetElement(obj, "class", "someclass", "status"); // ret "true" - if find element, "false" - does't
  GetElement(obj, "id", "someid", "click"); // ret "true" - if find element, "false" - does't
  
  
 
    
 * /


/*  Update Log

    10.01.2019:
    - Add limit of whait iterations on WhaitLoad
    - Add creator comments
    - Add "status" method for GetElement, WhaitLoad.
    - Update GetElement, WhaitLoad to "false" or "true" | "text" results.

    11.01.2019:
    - Add "clear" method to WhaitLoad, GetElement.
    - Add GetLength function.

    *ADD EXAMPLES

*/
