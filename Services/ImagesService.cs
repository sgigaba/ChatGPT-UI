using ChatGPT_UI.Interface;
using ChatGPT_UI.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ChatGPT_UI.Services
{
    public class ImagesService : ApiService<Images>, IImageApiService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ImagesService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public new DallEResponse DecodeDallEReponse(string body)
        {
            var model = JsonConvert.DeserializeObject<DallEResponse>(body);

            return model;
        }

        public new async Task<DallEResponse> GetAPIResponse(string prompt, string AImodel)
        {
            var client = httpClientFactory.CreateClient();
            var body = "";
            var model = new DallEResponse();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.openai.com/v1/images/generations"),
                Headers =
                {
                    { "Authorization", "Bearer OPENAIKEY" },
                },

                Content = new StringContent("{\n" +
                    "\"prompt\": \""+prompt+"\",\n" +
                    "\"n\": 1,\n" +
                    "\"size\": \"1024x1024\"\n" +
                    "}")
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                }
            };

            using (var response = await client.SendAsync(request))
            {
                if (response.ReasonPhrase == "Too Many Requests")
                {
                    Console.Write(response.Content);
                   // return (HandleBadRequest());
                }

                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
            }

            model = DecodeDallEReponse(body);

            return model;
        }

    }
}
