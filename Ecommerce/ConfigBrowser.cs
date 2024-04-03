using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace EcommerceAutomation;

public class ConfigBrowser : DriverManager
{
    public IWebDriver GetBrowser(string browserName)
    {
        switch (browserName)
        {
            case "chrome":
                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments("disable-notifications");
                // chromeOptions.AddArguments("--headless=false");
                return new ChromeDriver(chromeOptions);
            case "firefox":
                FirefoxOptions firefoxOptions = new FirefoxOptions();
                firefoxOptions.SetPreference("permissions.default.desktop-notification", 1);
                return new FirefoxDriver(firefoxOptions);
            case "edge":
                return new EdgeDriver();
            default:
                throw new ArgumentException("Invalid input!");
        }
    }
}