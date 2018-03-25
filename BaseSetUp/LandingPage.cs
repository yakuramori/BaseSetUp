using log4net;
using OpenQA.Selenium;

namespace BaseSetUp
{
    internal class LandingPage : AbstractNavigationBar
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string Title = "Identity Management | Omada Identity";
        private readonly By _searchInput = By.XPath("//form[@class='header__search']//input");
        private readonly By _cookieClose = By.XPath("//span[contains(@class, 'cookiebar__button') and contains(text(), 'Close')]");

        public LandingPage(IWebDriver driver) : base(driver)
        {
            WaitForPageTitleContains(Title);
            CloseCookieFooter();
        }

        public override bool IsPageLoaded()
        {
            return string.Equals(Title, GetPageTitle());
        }

        public SearchPage SearchFor(string text)
        {
            Log.Info($"Search for {text} on the page {Title}");
            Driver.FindElement(_searchInput).SendKeys(text);
            Driver.FindElement(_searchInput).Submit();
            return new SearchPage(Driver);
        }

        public LandingPage CloseCookieFooter()
        {
            ClickAtWebElementIfPresent(_cookieClose);
            return this;
        }
    }
}
