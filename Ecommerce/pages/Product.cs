using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace EcommerceAutomation;

public class ProductPage : PageUtilities
{
    [FindsBy(How = How.XPath, Using = "//label[contains(text(),'Size')]/following-sibling::div/select")]
    private readonly IWebElement _productSize;

    [FindsBy(How = How.Name, Using = "add")]
    private readonly IWebElement _addToCart;

    [FindsBy(How = How.CssSelector, Using = "h1.product-single__title")]
    private readonly IWebElement _productName;

    [FindsBy(How = How.CssSelector, Using = ".cart__item-name")]
    private readonly IWebElement _cartProductName;

    [FindsBy(How = How.CssSelector, Using = ".cart__item-title > div > div")]
    private readonly IWebElement _cartProductSize;

    [FindsBy(How = How.CssSelector, Using = ".btn.cart__checkout")]
    private readonly IWebElement _checkout;

    public ProductPage(IWebDriver driver) : base(driver)
    {
        PageFactory.InitElements(driver, this);
    }

    public void SelectTheSize(string size) => SelectByValue(_productSize, size);

    public string GetCartProductName() => WaitForElementToBeClickable(_cartProductName).Text;

    public string GetCartProductSize() => _cartProductSize.Text.Split(" ")[1];

    public CheckoutPage Checkout()
    {
        Click(WaitForElementToBeClickable(_checkout));
        return new CheckoutPage(_driver);
    }

    public string AddToCart()
    {
        Click(_addToCart);
        return _productName.Text;
    }
}