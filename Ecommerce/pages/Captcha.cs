using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace EcommerceAutomation;

public class Captcha : PageUtilities
{
    [FindsBy(How = How.Id, Using = "recaptcha-anchor")]
    private readonly IWebElement checkbox;

    [FindsBy(How = How.CssSelector, Using = "input[value='Submit']")]
    private readonly IWebElement submitBtn;

    public Captcha(IWebDriver driver) : base(driver)
    {
        PageFactory.InitElements(_driver, this);
    }

    public void HandleCaptchaIfRequired()
    {
        _wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath("//iframe[starts-with(@name, 'a-') and starts-with(@src, 'https://www.google.com/recaptcha')]")));
        _wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.recaptcha-checkbox-checkmark"))).Click();
    }
}