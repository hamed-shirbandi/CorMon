using CorMon.UITests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Microsoft.Extensions.DependencyInjection;

namespace CorMon.Web.Api.UITests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class ConfiguratuinTests :TestsBase
    {
        #region Fields
        
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
           
            appURL = Configuration[$"Environment:{EnvironmentName}:Project.Api.Url"];

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
        public void Check_Api_Version()
        {
            driver.Navigate().GoToUrl(appURL + "/Help/Check_Api_Version");
            var responseElement = driver.FindElement(By.TagName("pre"));
            Assert.IsTrue(responseElement.Text.Contains("1.0"), "Successfully initialized");

        }



        #endregion

        #region Cleanup




        [TestCleanup()]
        public void MyTestCleanup()
        {
            driver.Quit();
        }

        #endregion
    }
}
