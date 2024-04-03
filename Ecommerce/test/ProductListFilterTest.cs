namespace EcommerceAutomation
{
    [TestFixture]
    [Parallelizable]
    public class ProductListFilterTest : DriverManager
    {
        [Test]
        public void VerifyAllTheFiltersExistTest()
        {
            homepage.ClosePopup();
            homepage.OpenDrawer();
            homepage.ExpandMenSection();
            homepage.ExpandTeesSection();
            homepage.TeesSubMenuList();
            ProductsPage products = homepage.GoToPrintedTeesPage();
            List<string> listOfFilters = products.ListOfFilters();

            Assert.That(listOfFilters, Does.Contain("Sort"));
            Assert.That(listOfFilters, Does.Contain("Featured"));
            Assert.That(listOfFilters, Does.Contain("Best selling"));
            Assert.That(listOfFilters, Does.Contain("Alphabetically, A-Z"));
            Assert.That(listOfFilters, Does.Contain("Alphabetically, Z-A"));
            Assert.That(listOfFilters, Does.Contain("Price, low to high"));
            Assert.That(listOfFilters, Does.Contain("Price, high to low"));
            Assert.That(listOfFilters, Does.Contain("Date, old to new"));
            Assert.That(listOfFilters, Does.Contain("Date, new to old"));
        }

        [Test]
        public void AreProductsInExpectedOrderTest()
        {
            homepage.ClosePopup();
            homepage.OpenDrawer();
            homepage.ExpandMenSection();
            homepage.ExpandTeesSection();
            homepage.TeesSubMenuList();
            ProductsPage products = homepage.GoToPrintedTeesPage();
            products.SortProductsBy(SortBy.TitleDescending);

            Assert.IsTrue(
                products.ListOfProducts()
                .SequenceEqual(
                    products.ListOfProducts()
                    .OrderByDescending(x => x)
                )
            );
        }
    }
}