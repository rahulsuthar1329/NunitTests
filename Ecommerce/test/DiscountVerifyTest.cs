namespace EcommerceAutomation
{
    [TestFixture]
    [Parallelizable]
    public class DiscountVerificationTest : DriverManager
    {
        [Test]
        public void VerifyPrice()
        {
            homepage.ClosePopup();
            homepage.OpenSearchBar();
            ProductsPage products = homepage.SearchDealOfTheDay();
            products.SortProductsBy(SortBy.TitleAscending);
            List<Tuple<int, int, int>> productDetails = products.ExtractProductDetails();

            foreach (Tuple<int, int, int> product in productDetails)
            {
                Assert.That(
                    PageUtilities.CalculateDiscount(product.Item1, product.Item3),
                    Is.EqualTo(product.Item2).Within(5)
                );
            }
        }
    }
}