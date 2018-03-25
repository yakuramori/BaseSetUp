using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;

[assembly: log4net.Config.XmlConfigurator()]

namespace BaseSetUp
{
    [TestFixture]
    internal class TestWebPage
    {
        private IWebDriver _driver;
        private const string TestLinkText = "Gartner IAM Summit 2016 - London";
        private const string TestSearchText = "There is Safety in Numbers";
        private const string InitialPageUrl = @"https://www.omada.net";

        [SetUp]
        public void SetUp()
        {
            _driver = new BrowserFactory().GetDriver(Browser.Chrome);
        }

        [Test]
        public void TestSearch()
        {
            _driver.Navigate().GoToUrl(InitialPageUrl);
            var page = new LandingPage(_driver);
            Assert.Multiple((() =>
            {
                Assert.IsTrue(page.IsPageLoaded(), $"Incorrect page is opened. Was: {page.GetPageTitle()}");
                Assert.AreEqual("https://www.omada.net/", page.GetCurrentUrl(), "Landing Page URL is incorrect.");
                var searchPage = page.SearchFor("gartner");
                Assert.IsTrue(searchPage.IsPageLoaded(), $"Incorrect page is opened. Was: {page.GetPageTitle()}");
                var searchResulTextList = searchPage.GetSectionTextList();
                Assert.True(searchResulTextList.Count > 1, "Not enough search results. Should be more than 1.");
                Assert.True(searchResulTextList.Any(t => t.Contains(TestSearchText)), $"Required text \"{TestSearchText}\" is not found.");
                var articlePage = searchPage.OpenArticlePageByLink(TestLinkText);
                Assert.IsTrue(articlePage.IsPageLoaded(), $"Incorrect page is opened. Was: {page.GetPageTitle()}");
                Assert.IsTrue(articlePage.GetContentHeading().Contains(TestLinkText), $"Incorrect text in content header.");
            }));
        }

        [Test]
        public void TestNewsPage()
        {
            _driver.Navigate().GoToUrl(InitialPageUrl);
            var page = new LandingPage(_driver);
            Assert.IsTrue(page.IsPageLoaded(), $"Incorrect page is opened. Was: {page.GetPageTitle()}");
            var newsPage = page.NavigateToNewsPage();
            Assert.IsTrue(newsPage.IsPageLoaded(), $"Incorrect page is opened. Was: {newsPage.GetPageTitle()}");
            Assert.Contains(TestLinkText, newsPage.GetNewsHeadingList(), $"Text {TestLinkText} not found on page {newsPage.GetPageTitle()}");
        }

        [Test]
        public void TestContactPage()
        {
            var tabName = "U.S. West";
            _driver.Navigate().GoToUrl(InitialPageUrl);
            var page = new LandingPage(_driver);
            Assert.IsTrue(page.IsPageLoaded(), $"Incorrect page is opened. Was: {page.GetPageTitle()}");
            var contactsPage = page.NavigateToContactPage();
            Assert.IsTrue(contactsPage.IsPageLoaded(), $"Incorrect page is opened. Was: {contactsPage.GetPageTitle()}");
            contactsPage.ClickContactTab(tabName);
            contactsPage.TakeScreenshot().SaveAsFile(@"c:\temp\us-west.png");
            Assert.IsTrue(contactsPage.GetWebElementForContactTab(tabName).GetAttribute("class").Contains("selected"), 
                "Element attribute @class has not changed.");
            //hover over other tab and take screenshot
            contactsPage.MoveToElement(contactsPage.GetWebElementForContactTab("Germany"));
            contactsPage.TakeScreenshot().SaveAsFile(@"c:\temp\us-west-move-to.png");
        }

        [Test]
        public void TestPrivacyPage()
        {
            _driver.Navigate().GoToUrl(InitialPageUrl);
            var page = new LandingPage(_driver);
            Assert.IsTrue(page.IsPageLoaded(), $"Incorrect page is opened. Was: {page.GetPageTitle()}");
            var privacyPage = page.OpenPrivacyPage();
            Assert.IsTrue(privacyPage.IsPageLoaded(), $"Incorrect page is opened. Was: {privacyPage.GetPageTitle()}");
            //Close new Tab and switch to parent one.
            var handles = privacyPage.GetDriver().WindowHandles;
            privacyPage.Close();
            _driver.SwitchTo().Window(handles.First());
            page = new LandingPage(_driver);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(page.IsPageLoaded(), $"Incorrect page is opened. Was: {page.GetPageTitle()}");
                Assert.AreEqual(1, _driver.WindowHandles.Count, "Only one window handle should exist.");
            });
        }

        [Test]
        public void TestCasesPage()
        {
            _driver.Navigate().GoToUrl(InitialPageUrl);
            var page = new LandingPage(_driver);
            Assert.IsTrue(page.IsPageLoaded(), $"Incorrect page is opened. Was: {page.GetPageTitle()}");
            var casesPage = page.NavigateToCasesPage();
            Assert.IsTrue(casesPage.IsPageLoaded(), $"Incorrect page is opened. Was: {casesPage.GetPageTitle()}");
            var casePage = casesPage.NavigateToFirstCasePage();
            Assert.IsTrue(casePage.IsPageLoaded(), $"Incorrect page is opened. Was: {casePage.GetPageTitle()}");
            var downloadPage = casePage.PopulateRequestFormWith("zz@xx.com").SelectCountry("Albania").Accept().Slide().DownloadPdf();
            Assert.IsTrue(downloadPage.IsPageLoaded(), $"Incorrect page is opened. Was: {downloadPage.GetPageTitle()}");
            var fileName = downloadPage.GetDownloadFileName();
            downloadPage.DownloadCustomerCase();
            Assert.IsTrue(FileUtils.IfFileExistsInDownloadFolder(fileName), $"File: {fileName} is not present.");
        }

        [TearDown]
        public void TearDpwn()
        {
            var handles = _driver.WindowHandles;
            if (handles.Count == 2)
            {
                _driver.Close();
                _driver.SwitchTo().Window(handles.First());
            }
            _driver?.Close();
            _driver?.Quit();
        }

    }
}
