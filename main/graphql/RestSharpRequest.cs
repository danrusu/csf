using csf.main.utils;
using Flurl.Http;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace test.utils
{
    class RestSharpRequest
    {
        private readonly RestClient Client;
        private string Query;
        private string Variables;


        public RestSharpRequest(string url)
        {
            Client = new RestClient(url);
            this.Query = "";
            this.Variables = "{}";
        }

        public RestSharpRequest WithQuery(string query)
        {
            this.Query = query;
            return this;
        }

        public RestSharpRequest WithQueryFile(string queryFilePath)
        {
            this.Query = FileUtils.readResourceFileToString(queryFilePath);
            return this;
        }

        public RestSharpRequest WithVariables(object variables)
        {
            this.Variables = JsonConvert.SerializeObject(variables);
            return this;
        }


        public string Post()
        {
            Debug.WriteLine($"QUERY: {Query}\nVARIBLES: {Variables}");

            var request = new RestRequest()
                .AddJsonBody(new { query = Query, variables = Variables });

            IRestResponse response = Client.Post(request);

            string responseBody = response.Content;
            Debug.WriteLine($"RESPONSE: {responseBody}");

            return responseBody;
        }
    }        
}
