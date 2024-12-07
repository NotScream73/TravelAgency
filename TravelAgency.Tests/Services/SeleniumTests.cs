using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TravelAgency.Tests.Services
{
    [TestClass]
    public class MySeleniumTests
    {
        private TestContext testContextInstance;
        private IWebDriver driver;
        private string appURL;

        public MySeleniumTests()
        {
        }

        [TestMethod]
        public void TestCreatePage_Display()
        {
            driver.Navigate().GoToUrl("http://localhost:5216/Countries/Create");

            var title = driver.FindElement(By.TagName("h1")).Text;
            Assert.AreEqual("Добавление страны", title);

            var inputName = driver.FindElement(By.Id("Name"));
            Assert.IsNotNull(inputName);

            var submitButton = driver.FindElement(By.CssSelector("input[type='submit']"));
            Assert.IsNotNull(submitButton);
        }

        [TestMethod]
        public void TestCreateCountry_ValidData_Submit()
        {
            driver.Navigate().GoToUrl("http://localhost:5216/Countries/Create");

            var inputName = driver.FindElement(By.Id("Name"));
            inputName.SendKeys("Россия");

            var submitButton = driver.FindElement(By.CssSelector("input[type='submit']"));
            submitButton.Click();

            var title = driver.FindElement(By.TagName("h1")).Text;
            Assert.AreEqual("Список стран", title);
        }

        [TestMethod]
        public void TestIndexPage_Display()
        {
            driver.Navigate().GoToUrl("http://localhost:5216/Countries/Index");

            var title = driver.FindElement(By.TagName("h1")).Text;
            Assert.AreEqual("Список стран", title);

            var addButton = driver.FindElement(By.CssSelector("a.btn-success"));
            Assert.IsNotNull(addButton);
        }

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestInitialize()]
        public void SetupTest()
        {
            appURL = "http://www.bing.com/";

            driver = new ChromeDriver();
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            driver.Quit();
        }
    }
}
