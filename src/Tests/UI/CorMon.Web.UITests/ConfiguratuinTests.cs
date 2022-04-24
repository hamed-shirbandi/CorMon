using CorMon.UITests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace CorMon.Web.UITests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class ConfiguratuinTests :TestsBase
    {
        #region Fields

        private TestContext testContextInstance;
        private IWebDriver driver;
        private string appURL;

        #endregion

        #region Ctor

        public ConfiguratuinTests()
        {
        }

        #endregion

        #region Setup


        [TestInitialize()]
        public void SetupTest()
        {
            appURL = Configuration[$"Environment:{EnvironmentName}:Project.Web.Url"];

            string browser = "Chrome";
            switch (browser)
            {
                case "Chrome":
                    driver = new ChromeDriver(@"C:\Selenium_Drivers");// copy .exe files from \bin\Debug\netcoreapp2.1 to C:\Selenium_Drivers
                    break;
                case "Firefox":
                    driver = new FirefoxDriver();
                    break;
                case "IE":
                    driver = new InternetExplorerDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }

        }


        #endregion

        #region Tests




        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Check_App_Initialization()
        {
            driver.Navigate().GoToUrl(appURL + "/home/index");
            var responseElement = driver.FindElement(By.ClassName("uipasta-credit"));
            var tt = responseElement.Text;
            Assert.IsTrue(responseElement.Text.Contains("UiPasta"));// check if view contains UiPasta word


        }



        #endregion

        #region Helpers


        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }



        [TestCleanup()]
        public void MyTestCleanup()
        {
            driver.Quit();
        }

        #endregion

    }
}
