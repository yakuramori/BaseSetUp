using System.Linq;
using log4net;
using OpenQA.Selenium;

namespace BaseSetUp
{
    internal class CasesPage : AbstractNavigationBar
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string Title = "Customer Cases | Omada Identity Suite | Download Cases";
        private readonly By _genericDownloadLink = By.LinkText("Download PDF");

        public CasesPage(IWebDriver driver) : base(driver)
        {
            WaitForPageTitleContains(Title);
        }

        public override bool IsPageLoaded()
        {
            return string.Equals(Title, GetPageTitle());
        }

        public CasePage NavigateToFirstCasePage()
        {
            Log.Info("NavigateToFirstCasePage");
            FindWebElements(_genericDownloadLink).First().Click();
            return new CasePage(Driver);
        }

    }
}
