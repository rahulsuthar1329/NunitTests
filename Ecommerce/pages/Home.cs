using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace EcommerceAutomation;

public class HomePage : PageUtilities
{
    [FindsBy(How = How.Id, Using = "Search")]
    private readonly IWebElement _searchbar;

    [FindsBy(How = How.CssSelector, Using = ".js-drawer-open-nav")]
    private readonly IWebElement _drawerBtn;

    [FindsBy(How = How.CssSelector, Using = ".modal__centered >  .js-modal-close")]
    private readonly IWebElement _closePopup;

    [FindsBy(How = How.CssSelector, Using = ".site-nav__icons > *[href='/account']")]
    private readonly IWebElement _accountBtn;

    [FindsBy(How = How.CssSelector, Using = "a#Label-collections-mens3 + div > button")]
    private readonly IWebElement _menOption;

    [FindsBy(How = How.CssSelector, Using = "a#Sublabel-collections-mens-tees-15 + button")]
    private readonly IWebElement _teesOption;

    [FindsBy(How = How.CssSelector, Using = ".modal__inner button.modal__close")]
    private readonly IWebElement _popupCloseBtn;

    [FindsBy(How = How.CssSelector, Using = "#Sublinklist-collections-mens3-collections-mens-tees-15 li > a")]
    private readonly IList<IWebElement> _subListOfTees;

    [FindsBy(How = How.CssSelector, Using = "a[href='/collections/mens-tees-1']")]
    private readonly IWebElement _tees;

    [FindsBy(How = How.CssSelector, Using = "a[href='/collections/mens-printed-tees']")]
    private readonly IWebElement _printedTees;

    [FindsBy(How = How.CssSelector, Using = ".site-nav__icons a[href='/search']")]
    private readonly IWebElement _search;

    [FindsBy(How = How.CssSelector, Using = "#predictive-search-collections + ul a")]
    private readonly IWebElement _dealOfTheDay;

    [FindsBy(How = How.CssSelector, Using = ".predictive-search__no-results")]
    private readonly IWebElement _jacketAndHoodies;

    public HomePage(IWebDriver driver) : base(driver)
    {
        PageFactory.InitElements(driver, this);
    }

    public void GoToPage() => Navigate("/");

    public void OpenSearchBar() => Click(WaitForElementToBeClickable(_search));

    public void OpenDrawer() => Click(WaitForElementToBeClickable(_drawerBtn));

    public void ExpandMenSection() => Click(WaitForElementToBeClickable(_menOption));

    public void ExpandTeesSection() => Click(WaitForElementToBeClickable(_teesOption));

    public void PerformEscape() => Enumerable.Range(0, 3).ToList().ForEach(_ => _actions.SendKeys(Keys.Escape).Build().Perform());

    public List<string> TeesSubMenuList() => _subListOfTees.Select(element => element.GetAttribute("innerText").Trim()).ToList();

    public void ClosePopup()
    {
        _actions.SendKeys(Keys.Escape).Perform();
        ClickAnywhere();
    }

    public LoginPage RedirectToLoginPage()
    {
        Click(_accountBtn);
        return new LoginPage(_driver);
    }

    public ProductsPage SearchWomenHoodiesAndJackets()
    {
        SetValueToInputBox(_searchbar, "Womens hoodies and jackets");
        Click(WaitForElementToBeClickable(_jacketAndHoodies));
        return new ProductsPage(_driver);
    }

    public ProductsPage SearchDealOfTheDay()
    {
        SetValueToInputBox(_searchbar, "Deal of the Day");
        Click(WaitForElementToBeClickable(_dealOfTheDay));
        return new ProductsPage(_driver);
    }

    public ProductsPage GoToTeesPage()
    {
        Click(_tees);
        return new ProductsPage(_driver);
    }

    public ProductsPage GoToPrintedTeesPage()
    {
        Click(_printedTees);
        return new ProductsPage(_driver);
    }
}