using csf.main.utils;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace csf.test
{
    class SendungTest
    {
        string invalidResponse = @"{
            ""data"": {
                ""sendung"": null,
                ""other"": 1111
            }
        }";


        string validResponse = @"{
            ""data"": {
                ""sendung"": {
                    ""id"": 1111,
                    ""name"": ""letter""
                },
                ""other"": 1111
            }
        }";


        [Test]
        [Category("sendung")]
        public void TestSendungInvalidResponse()
        {
            var definition = new { data = new { sendung = new { id = "" } } };
            var dataObj = JsonConvert.DeserializeAnonymousType(invalidResponse, definition);
            var sendung = dataObj.data.sendung;
            Debug.WriteLine($"sendung: {sendung}");

            Assert.IsNull(sendung);
        }

        [Test]
        [Category("sendung")]
        public void TestSendungValidResponse()
        {
            var definition = new { data = new { sendung = new { id = "", name = "" } } };
            var dataObj = JsonConvert.DeserializeAnonymousType(validResponse, definition);
            var sendung = dataObj.data.sendung;
            Debug.WriteLine($"sendung: {sendung}");

            new SoftAssert().Add(
                () => Assert.AreEqual(111, Convert.ToInt32(sendung.id))).Add(
                () => Assert.AreEqual("lettr", sendung.name)
            ).AssertAll();
        }
    }
}
