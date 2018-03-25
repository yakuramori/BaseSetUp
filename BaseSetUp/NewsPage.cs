using System.Collections.Generic;
using System.Linq;
using log4net;
using OpenQA.Selenium;

namespace BaseSetUp
{
    internal class NewsPage : AbstractNavigationBar
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string Title = "News | Omada Identity Suite";
        private readonly By _genericNewsHeading = By.XPath("//section/h1[@class='cases__heading']");

        public NewsPage(IWebDriver driver) : base(driver)
        {
            WaitForPageTitleContains(Title);
        }

        public override bool IsPageLoaded()
        {
            return string.Equals(Title, GetPageTitle());
        }

        public List<string> GetNewsHeadingList()
        {
            Log.Info($"Prepare News heading text list from page {Title}");
            return new List<string>(FindWebElements(_genericNewsHeading).Select(elem => elem.Text));
        }
    }
}
