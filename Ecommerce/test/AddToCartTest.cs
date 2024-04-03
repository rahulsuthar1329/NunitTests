using TestData;

namespace EcommerceAutomation
{
    [TestFixture]
    [Parallelizable]
    public class AddToCartTest : DriverManager
    {
        [Test]
        public void VerifyProductDetailsInCartTest()
        {
            homepage.ClosePopup();
            homepage.OpenDrawer();
            homepage.ExpandMenSection();
            homepage.ExpandTeesSection();
            homepage.TeesSubMenuList();

            ProductsPage products = homepage.GoToPrintedTeesPage();
            ProductPage product = products.SelectTheProduct(TestVerificationData.TeeId328);
            product.SelectTheSize(TestVerificationData.ProductSize);
            string productName = product.AddToCart();

            Assert.That(product.GetCartProductName(), Is.EqualTo(productName));
            Assert.That(product.GetCartProductSize(), Is.EqualTo(TestVerificationData.ProductSize));
        }
    }
}