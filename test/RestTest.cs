using csf.main.models;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            List<string> EXPECTED_CONTINENT_NAMES = new List<string> { 
                "Africa", "Antarctica", "Asia", "Europe", 
                "North America", "Oceania", "South Americas" 
            };

            RestSharpRequest request = new RestSharpRequest(CONTINENTS_GRAPHQL_URL)
                .WithQueryFile(Path.Join("graphql", "continents.graphql"));

            string responseBody = request.Post();
            var definition = new { data = new { continents = new List<Continent>() } };
            var continentsObj = JsonConvert.DeserializeAnonymousType(responseBody, definition);

            List<Continent> continents = continentsObj.data.continents;
            Debug.WriteLine($"continents:\n{string.Join("\n", continents)}");

            List<string> continentNames = continents.Select(continent => continent.name).ToList();
            Debug.WriteLine($"continents names: {string.Join(", ", continentNames)}");

            Assert.AreEqual(EXPECTED_CONTINENT_NAMES, continentNames); // hard assert
        }

        [Test]
        [Category("graphql")]
        public void Test_ContinentEU_GraphQL_FlurlClient()
        {
            const string CONTINENTS_GRAPHQL_URL = "https://countries.trevorblades.com/";
            const string EXPECTED_CONTINENT_NAME = "Europe";
            const string EUROPE_CODE = "EU";

            FlurlRequest request = new FlurlRequest(CONTINENTS_GRAPHQL_URL)
                .WithQueryFile(Path.Join("graphql", "continent.graphql"))
                .WithVariables(new { code = EUROPE_CODE });

            string responseBody = request.Post();

            var continentDefinition = new { data = new { continent = new { name = "" } } };
            var continentObject = JsonConvert.DeserializeAnonymousType(responseBody, continentDefinition);
            Debug.WriteLine($"continent: {continentObject}");

            Assert.AreEqual(EXPECTED_CONTINENT_NAME, continentObject.data.continent.name);

            // countries array deserialization to IDictionary (or main.models.Country)
            var countriesDefinition = new
            {
                data = new
                {
                    continent = new
                    {
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

            var client = new RestSharp.RestClient("http://qatools.ro/api/");

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
