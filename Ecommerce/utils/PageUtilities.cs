using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TestData;

namespace EcommerceAutomation;

public class PageUtilities
{
    protected IWebDriver _driver;
    protected WebDriverWait _wait;
    protected Actions _actions;

    public PageUtilities(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        _actions = new Actions(_driver);
    }

    public bool IsElementClickable(IWebElement element)
    {
        try
        {
            return element.Displayed;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }

    public string GetCurrentUrl() => _driver.Url;

    public Action<IWebElement> Click = (element) => element.Click();

    public Action<IWebElement> Submit = (element) => element.Submit();

    public IWebElement FindElement(By locator) => _driver.FindElement(locator);

    public IList<IWebElement> FindElements(By locator) => _driver.FindElements(locator);

    public void ClickAnywhere() => _actions.MoveByOffset(10, 10).Click().Build().Perform();

    public void Navigate(string endPoint = "") => _driver.Navigate().GoToUrl(@$"{Configuration.BaseUrl}{endPoint}");

    public IWebElement WaitForElementToBeClickable(By locator) => _wait.Until(ExpectedConditions.ElementToBeClickable(locator));

    public IWebElement WaitForElementToBeClickable(IWebElement element) => _wait.Until(ExpectedConditions.ElementToBeClickable(element));

    public IList<IWebElement> WaitForAllElementsVisible(By locator) => _wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));

    public static int CalculateDiscount(int actualPrice, int discountPercent) => (int)Math.Round(actualPrice - (actualPrice * (double)discountPercent / 100));

    public void SelectByValue(IWebElement element, string optionValue) => new SelectElement(element).SelectByValue(optionValue);

    public void SelectByContent(IWebElement element, string option) => new SelectElement(element).SelectByText(option);

    public void ScrollToElement(IWebElement element) => _actions.MoveToElement(element).Perform();

    public void WaitForUrlContains(string url) => ExpectedConditions.UrlContains(url);

    public string GetRandomMobileNumber() => new Random().Next(6, 10) + TestVerificationData.GenerateRandomNumber(9);

    public string GetRandomCardNumber() => new Random().Next(1, 10) + TestVerificationData.GenerateRandomNumber(15);

    public Action<IWebElement, string> SetValueToInputBox = (inputBox, inputText) => inputBox.SendKeys(inputText);

    public string WaitForElementToHaveText(IWebElement element, string text)
    {
        _wait.Until(ExpectedConditions.TextToBePresentInElement(element, text));
        return element.Text;
    }

    public int ExtractNumber(string input)
    {
        Match match = Regex.Match(input.Replace(",", ""), @"(\d+)(\.\d+)?");

        if (match.Success)
        {
            string integerPart = match.Groups[1].Value;
            if (!string.IsNullOrEmpty(integerPart))
            {
                return int.Parse(integerPart);
            }
        }

        return 0;
    }
}