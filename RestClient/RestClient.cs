using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace RestClient
{

    class RestClient
    {
        static string Get(string endpoint)
        {
            string url = baseURL + endpoint;
            string result = "";

            var task = Task.Run<HttpResponseMessage>(async () => await httpClient.GetAsync(url));

            HttpResponseMessage response;
            if (task != null)
            {
                response = task.GetAwaiter().GetResult();
                Task<string> task2 = Task.Run<string>(async () => await response.Content.ReadAsStringAsync());
                result = task2.GetAwaiter().GetResult();
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new Exception($"Error from REST calling {url}: code {response.StatusCode}");
                }
            }

            return result;
        }

        static string baseURL = "http://localhost:8002/calc/";
        static HttpClient httpClient = new HttpClient();
        static void Main(string[] args)
        { 

            //mHttpClient.DefaultRequestHeaders.Accept.Clear();
            //mHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //httpClient.DefaultRequestHeaders.Add("ServerHost", "localhost");
            httpClient.DefaultRequestHeaders.Add("Source", "RestClient");

            var res = Get("add?a=10&b=20");
            Console.WriteLine(res);
            Console.Read();
        }
    }
}
