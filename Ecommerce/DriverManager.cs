using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using Microsoft.Extensions.Configuration;
using NUnit.Framework.Interfaces;

namespace EcommerceAutomation
{
    public class DriverManager
    {
        protected IConfigurationRoot config;
        protected Credentials credentials;
        protected IWebDriver driver;
        protected HomePage homepage;
        protected ExtentReporting report;

        [SetUp]
        public void StartBrowser()
        {
            report = new ExtentReporting();
            report.CreateTest(TestContext.CurrentContext.Test.Name);

            //configure credentials
            config = ConfigureCredentials();
            credentials = new Credentials(config);
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ConfigBrowser().GetBrowser(Environment.GetEnvironmentVariable("BrowserName"));

            homepage = new HomePage(driver);
            homepage.GoToPage();
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Manage().Window.Maximize();
        }

        private IConfigurationRoot ConfigureCredentials()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../.."))
                .AddJsonFile("./appsettings.json")
                .AddEnvironmentVariables()
                .Build();
        }

        protected string GetCurrentUrl() => driver.Url;

        [TearDown]
        public void CloseBrowser()
        {
            EndTest();
            report.EndReporting();
            driver.Quit();
        }

        private void EndTest()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;

            switch (testStatus)
            {
                case TestStatus.Failed:
                    report.LogFail($"Test has failed {message}");
                    break;
                case TestStatus.Skipped:
                    report.LogInfo($"Test has skipped {message}");
                    break;
                default:
                    break;
            }

            report.LogInfo("Test end");
            report.LogScreenshot("Ending Test with screenshot", ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString);
        }
    }
}