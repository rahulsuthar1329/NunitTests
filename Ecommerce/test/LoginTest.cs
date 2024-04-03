using TestData;

namespace EcommerceAutomation
{
    [TestFixture]
    [Ignore("Login needs recaptcha handling")]
    [Parallelizable]
    public class LoginPageTest : DriverManager
    {
        [Test]
        public void ShouldLoginWithValidCredentailsTest()
        {
            homepage.ClosePopup();
            LoginPage loginPage = homepage.RedirectToLoginPage();
            loginPage.InputCustomerCredentials(credentials.Username, credentials.Password);
            loginPage.SignIn();

            Assert.That(GetCurrentUrl(), Does.Contain(AuthRoutes.Account));
        }

        [Test]
        public void ShouldNotLoginWithInvalidCredentialsTest()
        {
            homepage.ClosePopup();
            LoginPage loginPage = homepage.RedirectToLoginPage();
            loginPage.InputCustomerCredentials("", "");
            loginPage.SignIn();

            Assert.That(GetCurrentUrl(), Does.Contain(AuthRoutes.Login));
            Assert.That(loginPage.GetErrorMessage(), Is.EqualTo("Incorrect email or password."));
        }
    }
}