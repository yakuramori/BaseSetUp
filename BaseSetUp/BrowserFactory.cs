using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace BaseSetUp
{
    internal class BrowserFactory
    {
        public int Timeout { get; set; } = 30;

        public IWebDriver GetDriver(Browser browser)
        {
            switch (browser)
            {
                case Browser.Chrome:
                    return GetChromeDriver(Timeout);
                case Browser.Firefox:
                    return GetFirefoxDriver(Timeout);
                default:
                    throw new ArgumentOutOfRangeException(nameof(browser), browser, null);
            }
        }

        private IWebDriver GetChromeDriver(int timeout)
        {
            var options = new ChromeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal,
                AcceptInsecureCertificates = true,
                UnhandledPromptBehavior = UnhandledPromptBehavior.Accept
            };
            var driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeout);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(timeout);
            driver.Manage().Window.Maximize();
            return driver;
        }

        private IWebDriver GetFirefoxDriver(int timeout)
        {
            var options = new FirefoxOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal,
                LogLevel = FirefoxDriverLogLevel.Info,
                AcceptInsecureCertificates = true,
                UnhandledPromptBehavior = UnhandledPromptBehavior.Accept
            };
            var driver = new FirefoxDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeout);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(timeout);
            return driver;
        }
    }
}
