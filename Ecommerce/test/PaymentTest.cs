using TestData;

namespace EcommerceAutomation
{
    [TestFixture]
    [Parallelizable]
    public class PaymentProcessTest : DriverManager
    {
        [Test]
        public void PaymentTest()
        {

            homepage.ClosePopup();
            homepage.OpenSearchBar();
            ProductsPage products = homepage.SearchWomenHoodiesAndJackets();
            ProductPage hoodie = products.SelectTheProduct(TestVerificationData.HoodieId33);
            hoodie.SelectTheSize(TestVerificationData.ProductSize);
            hoodie.AddToCart();

            CheckoutPage checkout = hoodie.Checkout();
            checkout.SetEmail(TestVerificationData.CustomerEmail);
            checkout.SetFirstName(TestVerificationData.CustomerFirstName);
            checkout.SetLastName(TestVerificationData.CustomerLastName);
            checkout.SetAddress(TestVerificationData.CustomerAddress);
            checkout.SetCity(TestVerificationData.CustomerCity);
            checkout.SelectState(TestVerificationData.CustomerState);
            checkout.SetPostalCode(TestVerificationData.CustomerPostalCode);
            checkout.SetPhoneNumber(TestVerificationData.CustomerPhoneNo);
            checkout.ContinueToShipping();

            PaymentPage payment = checkout.ContinueToPayment();
            Assert.That(payment.GetBrandName(), Is.EqualTo(TestVerificationData.BrandName));

            payment.SetMobileNumber();
            payment.Proceed();
            payment.PayThroughCard();

            payment.SetCardNumber();
            payment.SetCardHolderName();
            payment.SetCardExpiry();
            payment.SetCardCVV();
            payment.CancelTransaction();

            Assert.That(GetCurrentUrl(), Does.Contain(Configuration.BaseUrl));
        }
    }
}