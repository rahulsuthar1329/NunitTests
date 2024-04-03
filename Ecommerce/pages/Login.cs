using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace EcommerceAutomation;

public class LoginPage : PageUtilities
{
    [FindsBy(How = How.Id, Using = "CustomerEmail")]
    private readonly IWebElement _emailBox;

    [FindsBy(How = How.Id, Using = "CustomerPassword")]
    private readonly IWebElement _passwordBox;

    [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Sign In')]")]
    private readonly IWebElement _signInBtn;

    [FindsBy(How = How.CssSelector, Using = "a[href='/account/logout]'")]
    private readonly IWebElement _logout;

    [FindsBy(How = How.CssSelector, Using = ".errors")]
    private readonly IWebElement _loginError;

    public LoginPage(IWebDriver driver) : base(driver)
    {
        PageFactory.InitElements(driver, this);
    }

    public string GetErrorMessage() => _loginError.Text;

    public void GoToPage() => Navigate("/account/login");

    public void InputCustomerCredentials(string email, string password)
    {
        SetValueToInputBox(_emailBox, email);
        SetValueToInputBox(_passwordBox, password);
    }

    public void SignIn()
    {
        Click(_signInBtn);
        Click(new WebDriverWait(_driver, TimeSpan.FromSeconds(100)).Until(ExpectedConditions.ElementToBeClickable(_logout)));
    }
}