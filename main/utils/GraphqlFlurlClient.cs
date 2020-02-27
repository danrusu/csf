using Flurl.Http;
using System;
using System.Diagnostics;
using System.IO;

namespace test.utils
{
    class GraphqlFlurlClient
    {

        private string url;
        private FlurlClient client;

        public GraphqlFlurlClient(string url)
        {
            this.url = url;
            client = new FlurlClient(url);
        }

        private string queryFileToString(string queryFile)
        {
            return File.ReadAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName
                + @"\resources\" + queryFile);
        }

        public string post(string graphqlQueryFile, string variablesJson)
        {
            string query = queryFileToString(graphqlQueryFile);          

            Debug.WriteLine($"query = {query}");
            Debug.WriteLine($"variables = {variablesJson}");

            var response = client
                .AllowAnyHttpStatus()
                .Request()
                .PostJsonAsync(new { query, variables = variablesJson } )
                .Result;

            string responseBody = response.GetStringAsync().Result;

            Debug.WriteLine($"responseBody = {responseBody}");            

            return responseBody;
        }

        public string post(string graphqlQueryFile)
        {
            string query = queryFileToString(graphqlQueryFile);
            Debug.WriteLine($"query = {query}");

            var response = client
                .AllowAnyHttpStatus()
                .Request()
                .PostJsonAsync(new { query })
                .Result;

            string responseBody = response.GetStringAsync().Result;

            Debug.WriteLine($"responseBody = {responseBody}");

            return responseBody;
        }
    }
}
