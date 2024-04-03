using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using TestData;

namespace EcommerceAutomation;

public class PaymentPage : PageUtilities
{
    [FindsBy(How = How.XPath, Using = "//span[text()='Pay now']/parent::button")]
    private readonly IWebElement _payNow;

    [FindsBy(How = How.CssSelector, Using = ".logo-title-container p")]
    private readonly IWebElement _brandName;

    [FindsBy(How = How.CssSelector, Using = ".razorpay-checkout-frame")]
    private readonly IWebElement _razorpayIFrame;

    [FindsBy(How = How.Id, Using = "contact")]
    private readonly IWebElement _mobileNumber;

    [FindsBy(How = How.Id, Using = "redesign-v15-cta")]
    private readonly IWebElement _paymentProceed;

    [FindsBy(How = How.CssSelector, Using = "button[method='card']")]
    private readonly IWebElement _payThroughCard;

    [FindsBy(How = How.Id, Using = "card_number")]
    private readonly IWebElement _cardNumber;

    [FindsBy(How = How.Id, Using = "card_name")]
    private readonly IWebElement _cardHolderName;

    [FindsBy(How = How.Id, Using = "card_expiry")]
    private readonly IWebElement _cardExpiry;

    [FindsBy(How = How.Id, Using = "card_cvv")]
    private readonly IWebElement _cardCVV;

    [FindsBy(How = How.CssSelector, Using = "button.modal-close")]
    private readonly IWebElement _cancelTransaction;

    [FindsBy(How = How.CssSelector, Using = "#confirmation-dialog #positiveBtn")]
    private readonly IWebElement _acceptToCancelTxn;

    public PaymentPage(IWebDriver driver) : base(driver)
    {
        PageFactory.InitElements(driver, this);
    }

    public void PayNow() => Click(_payNow);

    public void Proceed() => Click(_paymentProceed);

    public void PayThroughCard() => Click(WaitForElementToBeClickable(_payThroughCard));

    public void SetMobileNumber() => SetValueToInputBox(_mobileNumber, GetRandomMobileNumber());

    public void SetCardNumber() => SetValueToInputBox(WaitForElementToBeClickable(_cardNumber), GetRandomCardNumber());

    public void SetCardHolderName() => SetValueToInputBox(_cardHolderName, "Rahul Suthar");

    public void SetCardExpiry() => SetValueToInputBox(_cardExpiry, "05/28");

    public void SetCardCVV() => SetValueToInputBox(_cardCVV, "432");

    public void CancelTransaction()
    {
        Click(_cancelTransaction);
        Click(WaitForElementToBeClickable(_acceptToCancelTxn));
        WaitForUrlContains(Configuration.BaseUrl);
    }

    public string GetBrandName()
    {
        _driver.SwitchTo().Frame(WaitForElementToBeClickable(_razorpayIFrame));
        return WaitForElementToHaveText(_brandName, TestVerificationData.BrandName);
    }
}