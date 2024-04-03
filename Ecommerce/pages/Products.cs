using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace EcommerceAutomation;

public class ProductsPage : PageUtilities
{
    [FindsBy(How = How.Id, Using = "SortBy")]
    private readonly IWebElement _filter;

    [FindsBy(How = How.CssSelector, Using = ".grid-product__title")]
    private readonly IList<IWebElement> _productTitles;

    [FindsBy(How = How.CssSelector, Using = ".page.current")]
    private readonly IWebElement _currentPage;

    [FindsBy(How = How.CssSelector, Using = ".pagination > :nth-last-child(2) > a")]
    private readonly IWebElement _lastPage;

    [FindsBy(How = How.CssSelector, Using = "span.next")]
    private readonly IWebElement _next;

    [FindsBy(How = How.CssSelector, Using = "span.prev")]
    private readonly IWebElement _prev;

    [FindsBy(How = How.CssSelector, Using = ".grid__item-image-wrapper > a")]
    private readonly IList<IWebElement> _productDetails;

    public ProductsPage(IWebDriver driver) : base(driver)
    {
        PageFactory.InitElements(driver, this);
    }

    public int CurrentPageNumber() => ExtractNumber(_currentPage.Text);

    public int LastPageNumber() => ExtractNumber(_lastPage.Text);

    public void GoToNextPage() => Click(_next);

    public void GoToPreviousPage() => Click(_prev);

    public void GoToLastPage() => Click(_lastPage);

    public bool IsPreviousButtonVisible() => IsElementClickable(_prev);

    public bool IsNextButtonVisible() => IsElementClickable(_next);

    public List<string> ListOfProducts() => _productTitles.Select(title => title.Text).ToList();

    public List<string> ListOfFilters() => _filter.FindElements(By.TagName("option")).Select(element => element.Text.Trim()).ToList();

    public ProductPage SelectTheProduct(string containingText)
    {
        Click(FindElement(By.XPath($"//div[contains(text(), '{containingText}')]/ancestor::a")));
        return new ProductPage(_driver);
    }

    public List<Tuple<int, int, int>> ExtractProductDetails()
    {
        List<Tuple<int, int, int>> productDetailsList = new List<Tuple<int, int, int>>();

        foreach (var product in _productDetails.Take(5))
        {
            productDetailsList.Add(
                new Tuple<int, int, int>(
                    ExtractNumber(product.GetAttribute("innerText").Split("\n")[2]),
                    ExtractNumber(product.GetAttribute("innerText").Split("\n")[4].Split(" ")[2]),
                    ExtractNumber(product.GetAttribute("innerText").Split("\n")[4].Split(" ")[4])
                )
            );
        }
        return productDetailsList;
    }

    public void SortProductsBy(SortBy sortBy)
    {
        SelectElement filterSelector = new SelectElement(_filter);

        switch (sortBy)
        {
            case SortBy.TitleAscending:
                filterSelector.SelectByValue("title-ascending");
                break;
            case SortBy.TitleDescending:
                filterSelector.SelectByValue("title-descending");
                break;
            default:
                break;
        }
    }
}