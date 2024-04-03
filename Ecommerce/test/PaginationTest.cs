using TestData;

namespace EcommerceAutomation
{
    [TestFixture]
    [Parallelizable]
    public class PaginationTest : DriverManager
    {
        [Test]
        public void ProductsShouldBeLessThan24Test()
        {
            homepage.ClosePopup();
            homepage.OpenDrawer();
            homepage.ExpandMenSection();
            homepage.ExpandTeesSection();
            homepage.TeesSubMenuList();
            ProductsPage products = homepage.GoToTeesPage();

            Assert.IsTrue(products.ListOfProducts().Count < 25);
        }

        [Test]
        public void PreviousButtonNotVisibleOnFirstPageTest()
        {
            homepage.ClosePopup();
            homepage.OpenDrawer();
            homepage.ExpandMenSection();
            homepage.ExpandTeesSection();
            homepage.TeesSubMenuList();
            ProductsPage products = homepage.GoToTeesPage();

            Assert.IsFalse(products.IsPreviousButtonVisible());
        }

        [Test]
        public void NextButtonNotVisibleOnLastPageTest()
        {
            homepage.ClosePopup();
            homepage.OpenDrawer();
            homepage.ExpandMenSection();
            homepage.ExpandTeesSection();
            homepage.TeesSubMenuList();
            ProductsPage products = homepage.GoToTeesPage();
            products.GoToLastPage();

            Assert.IsFalse(products.IsNextButtonVisible());
        }

        [Test]
        public void PageSwitchButtonsVisibleOnNonBoundaryPageTest()
        {
            homepage.ClosePopup();
            homepage.OpenDrawer();
            homepage.ExpandMenSection();
            homepage.ExpandTeesSection();
            homepage.TeesSubMenuList();
            ProductsPage products = homepage.GoToTeesPage();
            products.GoToNextPage();

            Assert.IsTrue(products.IsPreviousButtonVisible());
            Assert.IsTrue(products.IsNextButtonVisible());
        }

        [Test]
        public void CurrentPageShouldBeInUrlTest()
        {
            homepage.ClosePopup();
            homepage.OpenDrawer();
            homepage.ExpandMenSection();
            homepage.ExpandTeesSection();
            homepage.TeesSubMenuList();
            ProductsPage products = homepage.GoToTeesPage();
            products.GoToNextPage();

            Assert.That(GetCurrentUrl(), Does.Contain($"page={products.CurrentPageNumber()}"));
        }
    }
}