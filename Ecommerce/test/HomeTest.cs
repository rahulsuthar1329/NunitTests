namespace EcommerceAutomation
{
    [TestFixture]
    [Parallelizable]
    public class HomePageTest : DriverManager
    {
        [Test]
        public void VerifySubMenusInTeesTest()
        {
            homepage.ClosePopup();
            homepage.OpenDrawer();

            homepage.ExpandMenSection();
            homepage.ExpandTeesSection();
            List<string> subMenusOfTees = homepage.TeesSubMenuList();

            Assert.That(subMenusOfTees, Does.Contain("Printed Tees"));
            Assert.That(subMenusOfTees, Does.Contain("Pocket Tees"));
            Assert.That(subMenusOfTees, Does.Contain("Printed Oversize Tee"));
            Assert.That(subMenusOfTees, Does.Contain("Basic Oversize Tee"));
            Assert.That(subMenusOfTees, Does.Contain("Printed Cotton Tees"));
            Assert.That(subMenusOfTees, Does.Contain("Embroidered Tees"));
            Assert.That(subMenusOfTees, Does.Contain("Polo Tees"));
            Assert.That(subMenusOfTees, Does.Contain("Raglan Tees"));
            Assert.That(subMenusOfTees, Does.Contain("Printed Longline"));
        }
    }
}