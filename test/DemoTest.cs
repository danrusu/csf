using NUnit.Framework;
using System.Diagnostics;

namespace csf
{

    public class UnitTests2
    {
        [SetUp]
        public void BeforeEach()
        {
            Debug.WriteLine("before each 2");
        }

        [Test]
        [Ignore("skipped")]
        public void Test2()
        {
            Debug.WriteLine("test 2");
            Assert.Fail();
        }

        [TearDown]
        public void AfterEach()
        {
            Debug.WriteLine("after each 2");
        }
    }
}