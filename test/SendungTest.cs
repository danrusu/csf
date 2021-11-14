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
            var responseModel = new { data = new { sendung = new { id = "", name = "" } } };
            var response = JsonConvert.DeserializeAnonymousType(invalidResponse, responseModel);
            var sendung = response.data.sendung;
            Debug.WriteLine($"sendung: {sendung}");

            Assert.IsNotNull(sendung);
        }

        [Test]
        [Category("sendung")]
        public void TestSendungValidResponse()
        {
            var responseModel = new { data = new { sendung = new { id = "", name = "" } } };
            var response = JsonConvert.DeserializeAnonymousType(validResponse, responseModel);
            var sendung = response.data.sendung;
            Debug.WriteLine($"sendung: {sendung}");

            SoftAssert.Of(
                () => Assert.AreEqual(1111, Convert.ToInt32(sendung.id)),
                () => Assert.AreEqual("letter", sendung.name)
            ).AssertAll();
        }
    }
}
