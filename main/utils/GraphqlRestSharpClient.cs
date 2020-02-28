using csf.main.utils;
using Flurl.Http;
using RestSharp;
using System;
using System.Diagnostics;
using System.IO;

namespace test.utils
{
    class GraphqlRestSharpClient
    {

        private RestClient client;

        public GraphqlRestSharpClient(string url)
        {
            client = new RestSharp.RestClient(url);
        }


        public string post(string graphqlQueryFile, string variablesJson)
        {
            string query = FileUtils.readResourceFileToString(graphqlQueryFile);          

            Debug.WriteLine($"query = {query}\nvariables = {variablesJson}");

            var request = new RestSharp.RestRequest()
                .AddJsonBody(new { query, variables = variablesJson });

            IRestResponse response = client.Post(request);

            string responseBody = response.Content;
            Debug.WriteLine($"RESPONSE: {responseBody}");     

            return responseBody;
        }

        public string post(string graphqlQueryFile)
        {
            return post(graphqlQueryFile, "{}");
        }
    }
}
