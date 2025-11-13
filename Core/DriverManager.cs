using Core;
using OpenQA.Selenium;
using Selen;
using System;

public sealed class DriverManager
{
    private static DriverManager instance;
    private static readonly object padlock = new object();

    private IWebDriver driver;

    private DriverManager() { }

    public static DriverManager Instance
    {
        get
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new DriverManager();
                }
            }
            return instance;
        }
    }

    public IWebDriver Driver => driver;

    public void InitDriver(DriverType type)
    {
        if (driver != null)
        {
            driver.Quit();
            driver = null;
        }

        driver = WebDriverFactory.CreateWebDriver(type);
        driver.Manage().Window.Maximize();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
    }

    public void QuitDriver()
    {
        if (driver != null)
        {
            driver.Quit();
            driver = null;
        }
    }
}
