using log4net;
using OpenQA.Selenium;

namespace BaseSetUp
{
    internal class ArticlePage : AbstractNavigationBar
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
            Log.Warn($"Get text from element: {_contentHeading}");
            return FindWebElement(_contentHeading).Text;
        }
        
    }
}
