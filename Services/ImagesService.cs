using ChatGPT_UI.Interface;
using ChatGPT_UI.Models;
using System.Net.Http.Headers;

namespace ChatGPT_UI.Services
{
    public class ImagesService : ApiService<Images>, IImageService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ImagesService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public new async Task<ChatGPTResponse> GetAPIResponse(string prompt, string AImodel)
        {
            var client = httpClientFactory.CreateClient();
            var body = "";
            var model = new ChatGPTResponse();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.openai.com/v1/images/generations"),
                Headers =
                {
                    { "Authorization", "Bearer" },
                },

                Content = new StringContent("\n" +
                    "\"prompt\": \"A cute baby sea otter\",\n" +
                    "\n\":1,\n" +
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
                    return (HandleBadRequest());
                }

                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
            }

            model = DecodeAPIReponse(body);

            return model;
        }

    }
}
