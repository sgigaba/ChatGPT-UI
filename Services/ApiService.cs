namespace ChatGPT_UI.Services
{
    using System.Diagnostics.Metrics;
    using System.Net.Http.Headers;
    using System.Runtime.CompilerServices;

    using ChatGPT_UI.Models;

    public class ApiService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<Chat> GetChatGptResponse()
        {
            var client = httpClientFactory.CreateClient();

            client.DefaultRequestHeaders
                .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client
            .GetAsync(string
                .Format("http://api.weatherapi.com/v1/forecast.json?key=71de2c37ead844df82261931231404&q=england&days=3&aqi=no&alerts=no"))
                .Result;

            var responseString = await response.Content.ReadAsStringAsync();

            return null;
        }
    }
}
