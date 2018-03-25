using log4net;
using OpenQA.Selenium;

namespace BaseSetUp
{
    internal class PrivacyPage : AbstractNavigationBar
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string Title = "Omada | Processing of Personal Data";

        public PrivacyPage(IWebDriver driver) : base(driver)
        {
            WaitForPageTitleContains(Title);
        }

        public override bool IsPageLoaded()
        {
            return string.Equals(Title, GetPageTitle());
        }
    }
}
