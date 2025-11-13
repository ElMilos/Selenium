using Core;
using OpenQA.Selenium;
using Selen.Pages;


namespace Selen
{
    [TestClass]
    public sealed class MainTest
    {
        private IWebDriver driver;


        private void Setup(string webDriver)
        {
            Enum.TryParse(webDriver, true, out DriverType type);

            DriverManager.Instance.InitDriver(type);
            driver = DriverManager.Instance.Driver;

            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }


        [TestCleanup]
        public void TestCleanup()
        {
            DriverManager.Instance.QuitDriver();
        }


        [DataTestMethod]
        [DataRow("chrome")]
        [DataRow("edge")]
        public void UC1(string webDriver)
        {
            Setup(webDriver);

            var login = new LoginPage(driver);

            login.EnterPassword("smth");
            login.ClearPassword();
            login.ClickLogin();

            Assert.IsTrue(login.GetErrorText()
                .Contains("Epic sadface: Username is required"));
        }

        [DataTestMethod]
        [DataRow("chrome")]
        [DataRow("edge")]
        public void UC2(string webDriver)
        {
            Setup(webDriver);

            var login = new LoginPage(driver);

            login.EnterUsername("smth");
            login.ClickLogin();

            Assert.IsTrue(login.GetErrorText()
                .Contains("Epic sadface: Password is required"));
        }

        [DataTestMethod]
        [DataRow("chrome")]
        [DataRow("edge")]
        public void UC3(string webDriver)
        {
            Setup(webDriver);

            var login = new LoginPage(driver);
            login.EnterUsername("standard_user");
            login.EnterPassword("secret_sauce");
            login.ClickLogin();

            var inventory = new StorePage(driver);

            Assert.IsTrue(inventory.GetLogoText()
                .Contains("Swag Labs"));
        }
    }
}
