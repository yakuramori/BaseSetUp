using System.Collections.Generic;
using System.Linq;
using log4net;
using OpenQA.Selenium;

namespace BaseSetUp
{
    internal class SearchPage : AbstractNavigationBar
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string Title = "Search";
        private readonly By _genericSection = By.XPath("//div[@class='search-results__content']//section");
        private readonly By _genericSectionLinks = By.XPath("//div[@class='search-results__content']//section//a");

        public SearchPage(IWebDriver driver) : base(driver)
        {
            WaitForPageTitleContains(Title);
        }

        public override bool IsPageLoaded()
        {
            return string.Equals(Title, GetPageTitle());
        }

        public List<string> GetSectionTextList()
        {
            Log.Info($"Prepare found section text list from page {Title}");
            return new List<string>(FindWebElements(_genericSection).Select(elem => elem.Text));
        }

        public ArticlePage OpenArticlePageByLink(string linkText)
        {
            Log.Info($"Open article page by link text {linkText}");
            var linkElem = FindWebElements(_genericSectionLinks).Find(link => link.Text.Contains(linkText));
            ClickAtWebElement(linkElem);
            return new ArticlePage(Driver);
        }
    }
}
