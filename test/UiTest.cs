using csf.main.utils;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;

namespace csf
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
        public void DemoSkipped()
        {            
            Assert.Fail();
        }

        [Test]  
        public void DemoTest()
        {
            driver.Navigate().GoToUrl("http://www.danrusu.ro");    
            // actions, asserts
        }

        [TearDown]
        public void AfterEach()
        {
            driver.Quit();
        }
    }
}