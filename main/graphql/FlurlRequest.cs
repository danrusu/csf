using csf.main.utils;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace test.utils
{
    class FlurlRequest
    {
        private readonly FlurlClient Client;
        private string Query;
        private string Variables;


        public FlurlRequest(string url)
        {
            Client = new FlurlClient(url);
            Variables = "{}";
        }

        public FlurlRequest WithQuery(string query)
        {
            Query = query;
            return this;
        }

        public FlurlRequest WithQueryFile(string queryFilePath)
        {
            Query = FileUtils.readResourceFileToString(queryFilePath);
            return this;
        }

        public FlurlRequest WithVariables(object variables)
        {
            Variables = JsonConvert.SerializeObject(variables);
            return this;
        }

        public string Post()
        {
            Debug.WriteLine($"QUERY: {Query}\nVARIABLES: {Variables}");

            var response = Client
                .AllowAnyHttpStatus()
                .Request()
                .PostJsonAsync(new { query = Query, variables = Variables })
                .Result;

            string responseBody = response.GetStringAsync().Result;

            Debug.WriteLine($"RESPONSE: {responseBody}");

            return responseBody;
        }
    }
}
