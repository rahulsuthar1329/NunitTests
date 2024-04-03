using Microsoft.Extensions.Configuration;

namespace EcommerceAutomation;

public class Credentials
{
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string BrowserName { get; private set; }

    public Credentials(IConfigurationRoot config)
    {
        Username = config["Credentials:Username"];
        Password = config["Credentials:Password"];
        BrowserName = config["Driver:DriverName"];
    }
}