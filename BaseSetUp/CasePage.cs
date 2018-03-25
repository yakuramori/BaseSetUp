using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace BaseSetUp
{
    internal class CasePage : AbstractNavigationBar
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string Title = "Omada Customer Case | ";
        private readonly By _genericInput = By.XPath("//input[contains(@class, 'maxSize1')]");
        private readonly By _countrySelect = By.XPath("//select[@contactfield='address1_county']");
        private readonly By _acceptCheckbox = By.XPath("//input[@leadfield='new_omada_buddymail']");
        private readonly By _submitInput = By.Id("btnSubmit");
        private readonly By _slider = By.Id("Slider");

        public CasePage(IWebDriver driver) : base(driver)
        {
            WaitForPageTitleContains(Title);
        }

        public override bool IsPageLoaded()
        {
            return !string.IsNullOrEmpty(GetPageTitle()) && GetPageTitle().Contains(Title);
        }

        public CasePage PopulateRequestFormWith(string email)
        {
            Log.Info($"Populate all input fields with {email}");
            FindWebElements(_genericInput).ForEach(elem => elem.SendKeys(email));
            return this;
        }

        public CasePage SelectCountry(string country)
        {
            Log.Info($"Select country: {country}");
            new SelectElement(FindWebElement(_countrySelect)).SelectByText(country);
            return this;
        }

        public CasePage Accept()
        {
            ClickAtWebElement(FindWebElement(_acceptCheckbox));
            return this;
        }

        public CasePage Slide()
        {
            var slider = FindWebElement(_slider);
            new Actions(Driver)
                .MoveToElement(slider)
                .DragAndDropToOffset(slider, slider.Location.X + 150, slider.Location.Y)
                .Perform();
            return this;
        }

        public DownloadPage DownloadPdf()
        {
            ClickAtWebElement(FindWebElement(_submitInput));
            return new DownloadPage(Driver);
        }

    }
}
