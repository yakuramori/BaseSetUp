using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
#pragma warning disable 618

namespace BaseSetUp
{
    internal abstract class AbstractWebPage
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IWebDriver Driver;
        protected readonly int LoadTimeout = 30;
        private readonly WebDriverWait _wait;
        
        protected AbstractWebPage(IWebDriver driver)
        {
            Driver = driver;
            _wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(LoadTimeout));
        }
        public abstract bool IsPageLoaded();

        protected void WaitForPageTitleContains(string title)
        {
            Log.Debug($"Wait until page title is {title} for {LoadTimeout} seconds");
            _wait.Until(ExpectedConditions.TitleContains(title));
        }

        protected void WaitForElementVisible(By locator)
        {
            Log.Debug($"Wait until element {locator} is Visible for {LoadTimeout} seconds");
            _wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        protected void WaitForElementNotVisible(By locator)
        {
            Log.Debug($"Wait until element {locator} is Visible for {LoadTimeout} seconds");
            _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        public Screenshot TakeScreenshot()
        {
            return Driver.TakeScreenshot();
        }

        public string GetPageTitle()
        {
            return Driver.Title;
        }

        public string GetCurrentUrl()
        {
            return Driver.Url;
        }

        public List<IWebElement> FindWebElements(By locator)
        {
            Log.Debug($"Find elements by: {locator}");
            var elemList = new List<IWebElement>(Driver.FindElements(locator));
            if (elemList.Count == 0)
            {
                throw new NoSuchElementException($"No element found for {locator}");
            }
            Log.Debug($"{elemList.Count} elements found for {locator}");
            return elemList;
        }

        public IWebElement FindWebElement(By locator)
        {
            Log.Debug($"Find elements by: {locator}");
            var elems = Driver.FindElements(locator);
            
            if (elems.Count == 1)
            {
                return elems.First();
            }
            else
            {
                if (elems.Count == 0)
                {
                    throw new NoSuchElementException($"Element with locator {locator} not found.");
                }
                else
                {
                    throw new Exception($"More than one element found for locator {locator}");
                }
            }
        }

        public void ClickAtWebElement(IWebElement webElement)
        {
            var action = new Actions(Driver);
            action.MoveToElement(webElement).Perform();
            _wait.Until(ExpectedConditions.ElementToBeClickable(webElement));
            action.Click(webElement).Perform();
        }

        public void ClickAtWebElementIfVisible(By locator)
        {
            Log.Debug($"Find and click at element by: {locator}");
            var elemList = new List<IWebElement>(Driver.FindElements(locator));
            if (elemList.Count == 1)
            {
                if (elemList.First().Displayed)
                {
                    ClickAtWebElement(elemList.First());
                }
                else
                {
                    Log.Debug($"Found element by: {locator} but it is not visible.");
                }
                
            }
            else
            {
                if (elemList.Count == 0)
                {
                    throw new NoSuchElementException($"Element {locator} not found.");
                }
                else
                {
                    throw new Exception($"More than one element found for locator {locator}");
                }
            }
        }

        public void MoveToElement(By element)
        {
            var webElement = FindWebElement(element);
            new Actions(Driver).MoveToElement(webElement).Perform();
        }

        public void MoveToElement(IWebElement webElement)
        {
            new Actions(Driver).MoveToElement(webElement).Perform();
        }

        public void Close()
        {
            Log.Debug($"Close current page with handle: {Driver.CurrentWindowHandle}");
            Driver.Close();
        }

        public IWebDriver GetDriver()
        {
            return Driver;
        }
    }
}
