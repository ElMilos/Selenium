using Core;
using OpenQA.Selenium;
using Core.Pages;

namespace Tests
{
    [TestClass]
    public sealed class MainTest
    {
        private IWebDriver? driver;

        [TestInitialize]
        public void Setup()
        {
            string? browser = Config.Configuration["Browser:Chrome"];
            string? url = Config.Configuration["Urls:Url"];

            Enum.TryParse(browser, true, out DriverType type);

            DriverManager.Instance.InitDriver(type);
            driver = DriverManager.Instance.Driver;

            if (url is null)
                throw new InvalidOperationException("BaseUrl is missing in configuration.");

            driver.Navigate().GoToUrl(url);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DriverManager.Instance.QuitDriver();
        }


        [TestMethod]
        public void Login_NoUsername_ShowsUsernameRequiredMessage()
        {
            var login = new LoginPage(driver);

            login.EnterPassword("smth");
            login.ClearPassword();
            login.ClickLogin();

            Assert.IsTrue(
                login.GetErrorText().Contains("Username is required"),
                "Expected username required message."
            );
        }


        [TestMethod]
        public void Login_NoPassword_ShowsPasswordRequiredMessage()
        {
            var login = new LoginPage(driver);

            login.EnterUsername("smth");
            login.ClickLogin();

            Assert.IsTrue(
                login.GetErrorText().Contains("Password is required"),
                "Expected password required message."
            );
        }


        [TestMethod]
        public void Login_ValidCredentials_NavigatesToInventory()
        {
            var login = new LoginPage(driver);

            var username = Config.Configuration["Credentials:Username"];
            var password = Config.Configuration["Credentials:Password"];

            login.EnterUsername(username);
            login.EnterPassword(password);
            login.ClickLogin();

            var inventory = new StorePage(driver);

            Assert.IsTrue(
                inventory.GetLogoText().Contains("Swag Labs"),
                "Inventory page logo text mismatch."
            );
        }
    }
}
