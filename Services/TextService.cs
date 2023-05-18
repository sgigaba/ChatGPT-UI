using ChatGPT_UI.Interface;
using ChatGPT_UI.Models;
using System.Net.Http.Headers;

namespace ChatGPT_UI.Services
{
    public class TextService : ApiService<Texts>, ITextService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public TextService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
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
                RequestUri = new Uri("https://api.openai.com/v1/completions"),
                Headers =
                {
                    { "Authorization", "Bearer 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" },
                },

                Content = new StringContent("{\n" +
                    "\"model\": \"text-davinci-003\",\n" +
                    "\"prompt\": \""+prompt+"\",\n" +
                    "\"max_tokens\": 7,\n" +
                    "\"temperature\": 0,\n" +
                    "\"top_p\": 1,\n" +
                    "\"n\": 1,\n" +
                    "\"stream\": false,\n" +
                    "\"logprobs\":null,\n" +
                    "\"stop\": \"\"\n}")
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
