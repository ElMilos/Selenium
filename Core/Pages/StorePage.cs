using OpenQA.Selenium;
using System.Threading;


namespace Core
{
    public class StorePage
    {
        private readonly IWebDriver _driver;

        public StorePage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement Logo
        {
            get
            {
                Thread.Sleep(1000);
                return _driver.FindElement(By.ClassName("app_logo"));
            }
        }

        public string GetLogoText() => Logo.Text;
    }
}