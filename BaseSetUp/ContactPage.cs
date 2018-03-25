using log4net;
using OpenQA.Selenium;

namespace BaseSetUp
{
    internal class ContactPage : AbstractNavigationBar
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string Title = "Contact Omada | Leading Provider of IT Security Solutions";
        private string _genericContactTab = "//div[@class='tabmenu__menu']/span[.='%%']";

        public ContactPage(IWebDriver driver) : base(driver)
        {
            WaitForPageTitleContains(Title);
        }

        public override bool IsPageLoaded()
        {
            return string.Equals(Title, GetPageTitle());
        }

        public ContactPage ClickContactTab(string tabName)
        {
            Log.Info($"Click on Tab: {tabName}");
            var xPath = _genericContactTab.Replace("%%", tabName);
            ClickAtWebElement(FindWebElement(By.XPath(xPath)));
            return this;
        }

        public IWebElement GetWebElementForContactTab(string tabName)
        {
            Log.Info($"Get web element for Tab: {tabName}");
            var xPath = _genericContactTab.Replace("%%", tabName);
            return FindWebElement(By.XPath(xPath));
        }
    }
}
