using System.Diagnostics;
using csf.main.utils;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace csf.test
{

    [Category("UI")]
    public class UITest
    {
        ChromeDriver driver;

        [SetUp]
        public void BeforeEach()
        {
            driver = new ChromeDriver(FileUtils.getFullResourcePath("drivers"));
        }

        [Test]
        [Ignore("skipped")]
        public void UiDemoSkippedTest()
        {            
            Assert.Fail();
        }

        [Test]
        public void UiDemoTest()
        {
            driver.Navigate().GoToUrl("http://www.qatools.ro");
            // actions, asserts
        }

        [TearDown]
        public void AfterEach()
        {
            driver.Quit();
        }
    }
}