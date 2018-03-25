using log4net;
using OpenQA.Selenium;

namespace BaseSetUp
{
    internal class DownloadPage : AbstractNavigationBar
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string Title = "Download | ";
        private readonly By _genericDownloadLink = By.PartialLinkText("Download Customer Case:");

        public DownloadPage(IWebDriver driver) : base(driver)
        {
            WaitForPageTitleContains(Title);
        }

        public override bool IsPageLoaded()
        {
            return !string.IsNullOrEmpty(GetPageTitle()) && GetPageTitle().Contains(Title);
        }

        public DownloadPage DownloadCustomerCase()
        {
            var downloadLink = FindWebElement(_genericDownloadLink);
            Log.Info($"Click on link: {downloadLink.Text}");
            ClickAtWebElement(downloadLink);
            return this;
        }
    }
}
