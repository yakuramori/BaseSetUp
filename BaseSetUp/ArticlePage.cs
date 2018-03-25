using OpenQA.Selenium;

namespace BaseSetUp
{
    internal class ArticlePage : AbstractNavigationBar
    {
        private readonly By _contentHeading = By.XPath("//section[@class='text__content']/h1[@class='text__heading']");
        private readonly string _url = "/more/news-events/news/";

        public ArticlePage(IWebDriver driver) : base(driver)
        {
            WaitForElementVisible(_contentHeading);
        }

        public override bool IsPageLoaded()
        {
            return GetCurrentUrl().Contains(_url);
        }

        public string GetContentHeading()
        {
            return FindWebElement(_contentHeading).Text;
        }
        
    }
}
