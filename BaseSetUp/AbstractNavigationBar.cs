using System.Collections.Generic;
using System.Linq;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace BaseSetUp
{
    internal abstract class AbstractNavigationBar : AbstractWebPage
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly By _navigationMoreLink = By.LinkText("More...");
        private readonly By _navigationNewsLink = By.XPath("//a[@class='header__menulink--submenu' and .='News']");
        private readonly By _navigationContactLink = By.XPath("//a[@class='footer__menulink--submenu' and .='Contact']");
        private readonly By _navigationPrivacyPolicyLink = By.XPath("//a[@class='footer__menulink--submenu' and .='Privacy Policy']");
        private readonly By _navigationCasesLink = By.XPath("//a[@class='footer__menulink--submenu' and .='Cases']");

        protected AbstractNavigationBar(IWebDriver driver) : base(driver)
        {
        }

        #region Page Header

        public NewsPage NavigateToNewsPage()
        {
            var action = new Actions(Driver);
            action.MoveToElement(Driver.FindElement(_navigationMoreLink)).Perform();
            WaitForElementVisible(_navigationNewsLink);
            action.MoveToElement(Driver.FindElement(_navigationNewsLink))
                .Click(Driver.FindElement(_navigationNewsLink))
                .Build().Perform();
            return new NewsPage(Driver);
        }

        #endregion

        #region Page Footer

        public ContactPage NavigateToContactPage()
        {
            Log.Info($"Navigate to ContactPage by Link: {_navigationContactLink}");
            ClickAtWebElement(FindWebElement(_navigationContactLink));
            return new ContactPage(Driver);
        }

        public CasesPage NavigateToCasesPage()
        {
            Log.Info($"Navigate to CasesPage by Link: {_navigationCasesLink}");
            ClickAtWebElement(FindWebElement(_navigationCasesLink));
            return new CasesPage(Driver);
        }

        public PrivacyPage OpenPrivacyPage()
        {
            var action = new Actions(Driver);
            action.KeyDown(Keys.Control)
                .MoveToElement(FindWebElement(_navigationPrivacyPolicyLink))
                .Click().KeyUp(Keys.Control).Perform();
            var tabs = new List<string>(Driver.WindowHandles);
            Driver.SwitchTo().Window(tabs.Last());
            return new PrivacyPage(Driver);
        }

        #endregion

    }
}
