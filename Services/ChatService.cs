namespace ChatGPT_UI.Services
{
    using ChatGPT_UI.Interface;
    using ChatGPT_UI.Models;
    using System.Net.Http;
    using System.Net.Http.Headers;

    public class ChatService : ApiService<Chats>, IChatService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ChatService(IHttpClientFactory httpClientFactory) 
            : base(httpClientFactory)
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
                RequestUri = new Uri("https://api.openai.com/v1/chat/completions"),
                Headers =
                {
                    { "Authorization", "Bearer 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" },
                },

                Content = new StringContent("{\n" +
                   "\"model\":\"" + AImodel + "\", \n" +
                   "\"messages\": [\n" +
                   "{\n" +
                   "\"role\": \"user\",\n" +
                   "\"content\": \"" + prompt + "\"\n" +
                   "}\n" +
                   "]\n" +
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
