using Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;

namespace Selen
{
    public class WebDriverFactory
    {
        public static IWebDriver CreateWebDriver(DriverType webDriver)
        {
            IWebDriver driver;
            switch (webDriver)
            {
                case DriverType.CHROME:
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--headless=new");
                    chromeOptions.AddArgument("--start-maximized");
                    chromeOptions.AddArgument("--disable-notifications");
                    chromeOptions.AddArgument("--disable-popup-blocking");
                    driver = new ChromeDriver(chromeOptions);
                    break;
                case DriverType.EDGE:
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.AddArgument("--headless=new");
                    edgeOptions.AddArgument("--start-maximized");
                    edgeOptions.AddArgument("--disable-notifications");
                    driver = new EdgeDriver(edgeOptions);
                    break;
                default:
                    throw new ArgumentException($"Unsupported web driver: {webDriver}");
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            driver.Manage().Cookies.DeleteAllCookies();

            return driver;
        }
    }
}
