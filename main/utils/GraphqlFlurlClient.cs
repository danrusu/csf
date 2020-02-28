using csf.main.utils;
using Flurl.Http;
using System;
using System.Diagnostics;
using System.IO;

namespace test.utils
{
    class GraphqlFlurlClient
    {

        private FlurlClient client;

        public GraphqlFlurlClient(string url)
        {
            client = new FlurlClient(url);
        }

        public string post(string graphqlQueryFile, string variablesJson)
        {
            string query = FileUtils.readResourceFileToString(graphqlQueryFile);

            Debug.WriteLine($"query = {query}\nvariables = {variablesJson}");

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
            return post(graphqlQueryFile, "{}");
        }
    }
}
