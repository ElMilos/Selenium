using OpenQA.Selenium;

namespace Core.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement UsernameInput => _driver.FindElement(By.Id("user-name"));
        private IWebElement PasswordInput => _driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => _driver.FindElement(By.Id("login-button"));
        private IWebElement ErrorMessage => _driver.FindElement(By.CssSelector("[data-test='error']"));

        public void EnterUsername(string username) => UsernameInput.SendKeys(username);
        public void EnterPassword(string password) => PasswordInput.SendKeys(password);
        public void ClearPassword() => PasswordInput.Clear();
        public void ClickLogin() => LoginButton.Click();
        public string GetErrorText() => ErrorMessage.Text;
    }
}