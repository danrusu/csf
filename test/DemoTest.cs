using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;

namespace csf
{

    [Category("UI")]
    public class DemoTest
    {
        ChromeDriver driver;

        [SetUp]
        public void BeforeEach()
        {
            driver = new ChromeDriver(getWebDriversPath("drivers"));
        }

        [Test]
        [Ignore("skipped")]
        public void DemoSkipped()
        {
            Debug.WriteLine("test 2");
            Assert.Fail();
        }

        [Test]  
        public void UITest()
        {
            driver.Navigate().GoToUrl("http://www.danrusu.ro");    
            // actions, asserts
        }

        private ChromeOptions getWebDriversPath(string v)
        {
            throw new NotImplementedException();
        }

        [TearDown]
        public void AfterEach()
        {
            driver.Quit();
        }
    }
}