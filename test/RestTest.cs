using csf.main.models;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using test;
using test.utils;

namespace csf
{
    [Category("API")]
    public class RestTest
    {

        [Test]
        [Category("graphql")]
        public void Test_Continents_GraphQL_RestSharpClient()
        {
            const string CONTINENTS_GRAPHQL_URL = "https://countries.trevorblades.com/";
  
            Action login = () =>
            {
                GraphqlRestSharpClient graphql = new GraphqlRestSharpClient(CONTINENTS_GRAPHQL_URL);

                string responseBody = graphql.post(@"graphql\continents.graphql");
            };

            // TimeUtils.getDuration usage demo 
            long duration = TimeUtils.getDuration(login);
        }

        [Test]
        [Category("graphql")]
        public void Test_ContinentEU_GraphQL_FlurlClient()
        {
            const string EXPECTED_CONTINENT_NAME = "Europe";

            const string CONTINENTS_GRAPHQL_URL = "https://countries.trevorblades.com/";
            string code = "EU";

            GraphqlFlurlClient graphql = new GraphqlFlurlClient(CONTINENTS_GRAPHQL_URL);

            var continentVariables = new { code };

            string continentVariablesJson = JsonConvert.SerializeObject(continentVariables);

            string responseBody = graphql.post(@"graphql\continent.graphql", continentVariablesJson);

            var continentDefinition = new { data = new { continent = new { name = "" } } };

            var continentObject = JsonConvert.DeserializeAnonymousType(responseBody, continentDefinition);
            Debug.WriteLine($"continent: {continentObject}");

            Assert.AreEqual(EXPECTED_CONTINENT_NAME, continentObject.data.continent.name);

            // countries array deserialization to IDictionary (or main.models.Country)
            var countriesDefinition = new { 
                data = new { 
                    continent = new { 
                        countries = new List<Country>()
                        // countries = new List<IDictionary>() 
                    }
                } 
            };

            var countriesObject = JsonConvert.DeserializeAnonymousType(responseBody, countriesDefinition);
            Debug.WriteLine($"countriesObject: {countriesObject}");
            
            List<Country> countries = countriesObject.data.continent.countries;
            Debug.WriteLine($"countries: {String.Join(",", countries)}");

            const string EXPECTED_FIRST_COUNTRY_NAME = "Andorra";
            Assert.AreEqual(
                EXPECTED_FIRST_COUNTRY_NAME,
                countries[0].name);            
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

    }
}
