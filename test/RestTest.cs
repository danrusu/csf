using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Diagnostics;
using test;
using test.utils;

namespace csf
{
    [TestFixture]
    [Category("API")]
    public class RestTest
    {
        [SetUp]
        public void BeforeEach()
        {
            Debug.WriteLine("before each 1");
        }

        [Test]
        public void TestContinentsGraphQL()
        {
            const string CONTINENTS_GRAPHQL_URL = "https://countries.trevorblades.com/";
            string email = "dan.rusu @rms.com";
            string password = "wrongPassword";

            Action login = () => {
                GraphqlFlurlClient graphql = new GraphqlFlurlClient(CONTINENTS_GRAPHQL_URL);

                var loginVariables = new { email, password };

                string loginVariablesJson = JsonConvert.SerializeObject(loginVariables);

                string responseBody = graphql.post(@"graphql\continents.graphql");
            };

            long duration = TimeUtils.getDuration(login);
        }

        [Test]
        public void TestContinentEUGraphQL()
        {
            const string CONTINENTS_GRAPHQL_URL = "https://countries.trevorblades.com/";
            string code = "EU";

            Action login = () => {
                GraphqlFlurlClient graphql = new GraphqlFlurlClient(CONTINENTS_GRAPHQL_URL);

                var continentVariables = new { code };

                string continentVariablesJson = JsonConvert.SerializeObject(continentVariables);

                string responseBody = graphql.post(@"graphql\continent.graphql", continentVariablesJson);
            };

            long duration = TimeUtils.getDuration(login);
            Assert.Pass();
        }

        [Test]
        [Category("calculate")]
        public void TestRestSharp()
        {
            const string EXPECTED_RESULT = "25";

            var client = new RestSharp.RestClient("http://danrusu.ro/api/");

            var request = new RestSharp.RestRequest("calculate.php")
                .AddQueryParameter("firstNumber", "5")
                .AddQueryParameter("secondNumber", "5")
                .AddQueryParameter("operation", "2");

            IRestResponse response = client.Get(request);

            Debug.WriteLine($"RESPONSE: {response.Content}");

            // DESERIALIZE via Newtonsoft.Json

            // FULL deserialization model
            CalculateResponse calculateResponse = JsonConvert.DeserializeObject<CalculateResponse>(response.Content);
            Debug.WriteLine($"response: {calculateResponse}");
            Assert.AreEqual(EXPECTED_RESULT, calculateResponse.Result);

            // PARTIAL deserialization - use Anonymus type or IDictionary
            var definition = new { result = "" };
            Debug.WriteLine($"definition: {definition.GetType().Name}");

            //IDictionary dictionary = new Dictionary<string, string>();
            //dictionary.Add("result", "");            

            var calculateResponsePartial = JsonConvert.DeserializeAnonymousType(response.Content, definition);
            //var calculateResponsePartial = JsonConvert.DeserializeAnonymousType(response.Content, dictionary);
            Debug.WriteLine($"partial response: {calculateResponsePartial}");

            Assert.AreEqual(EXPECTED_RESULT, calculateResponsePartial.result);
            //Assert.AreEqual(EXPECTED_RESULT, calculateResponsePartial["result"]);
        }

        [TearDown]
        public void AfterEach()
        {
            Debug.WriteLine("after each 1");
        }
    }
}