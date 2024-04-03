using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace EcommerceAutomation;

public class CheckoutPage : PageUtilities
{
    [FindsBy(How = How.Id, Using = "marketing_opt_in")]
    private readonly IWebElement _notifyMeCheckbox;

    [FindsBy(How = How.Id, Using = "email")]
    private readonly IWebElement _customerEmail;

    [FindsBy(How = How.Name, Using = "zone")]
    private readonly IWebElement _customerState;

    [FindsBy(How = How.Name, Using = "firstName")]
    private readonly IWebElement _firstName;

    [FindsBy(How = How.Name, Using = "lastName")]
    private readonly IWebElement _lastName;

    [FindsBy(How = How.Name, Using = "address1")]
    private readonly IWebElement _customerAddress;

    [FindsBy(How = How.Name, Using = "city")]
    private readonly IWebElement _customerCity;

    [FindsBy(How = How.Name, Using = "postalCode")]
    private readonly IWebElement _customerPostalCode;

    [FindsBy(How = How.Name, Using = "phone")]
    private readonly IWebElement _customerPhone;

    [FindsBy(How = How.XPath, Using = "//span[text()='Continue to shipping']/parent::button")]
    private readonly IWebElement _continueToShipping;

    [FindsBy(How = How.XPath, Using = "//span[text()='Continue to payment']/parent::button")]
    private readonly IWebElement _continueToPayment;

    [FindsBy(How = How.XPath, Using = "//span[text()='Pay now']/parent::button")]
    private readonly IWebElement _payNow;

    public CheckoutPage(IWebDriver driver) : base(driver)
    {
        PageFactory.InitElements(driver, this);
    }

    public void SetEmail(string email) => SetValueToInputBox(WaitForElementToBeClickable(_customerEmail), email);

    public void SetFirstName(string firstName) => SetValueToInputBox(_firstName, firstName);

    public void SetLastName(string lastName) => SetValueToInputBox(_lastName, lastName);

    public void SetAddress(string address) => SetValueToInputBox(_customerAddress, address);

    public void SetCity(string city) => SetValueToInputBox(_customerCity, city);

    public void SetPostalCode(string postalCode) => SetValueToInputBox(_customerPostalCode, postalCode);

    public void SetPhoneNumber(string phone) => SetValueToInputBox(_customerPhone, phone);

    public void ToggleNotifyMeCheckbox() => Click(_notifyMeCheckbox);

    public void ContinueToShipping() => Click(WaitForElementToBeClickable(_continueToShipping));

    public PaymentPage ContinueToPayment()
    {
        Click(WaitForElementToBeClickable(_continueToPayment));
        Click(WaitForElementToBeClickable(_payNow));
        return new PaymentPage(_driver);
    }

    public void SelectState(string state)
    {
        Click(_customerState);
        SelectByContent(_customerState, state);
    }
}