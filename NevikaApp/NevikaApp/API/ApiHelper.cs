using NevikaApp.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace NevikaApp.API
{
    public static class ApiHelper
    {
        public static HttpClient ApiClient { get; private set; }

        /// <summary>
        /// Initializes the HttpClient
        /// </summary>
        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            // Using the connection string found in Constants
            ApiClient.BaseAddress = new Uri(Constants.BaseURL);
            // 8 seconds timeout
            ApiClient.Timeout = TimeSpan.FromSeconds(8);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
