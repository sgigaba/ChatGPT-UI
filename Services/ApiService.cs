namespace ChatGPT_UI.Services
{
    using System.Diagnostics.Metrics;
    using System.Net.Http.Headers;
    using System.Net.Mime;
    using System.Reflection.PortableExecutable;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ChatGPT_UI.Models;
    using Newtonsoft.Json;

    public class ApiService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public ChatGPTResponse DecodeChatGptResponse(string body)
        {
            var chatGTPResponse = JsonConvert.DeserializeObject<ChatGPTResponse>(body);
            
            return chatGTPResponse;
        }

        public ChatGPTResponse HandleBadRequest()
        {
            var choices = new List<Choices>();

            var choice = new Choices()
            {
                message = new Message()
                {
                    content = "ChatGPT has too many requests at the moment. Please try again soon"
                }
            };

            choices.Add(choice);

            var model = new ChatGPTResponse()
            {
                id = "null",
                choices = choices

            };

            return model;
        }


        public async Task<ChatGPTResponse> GetChatGptResponse(string prompt)
        {
            var client = httpClientFactory.CreateClient();
            var model = new ChatGPTResponse();
            var body = "";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.openai.com/v1/chat/completions"),
                Headers =
                {
                    { "Authorization", "Bearer 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" },
                },
                Content = new StringContent
                    ("{\n \"model\": \"gpt-3.5-turbo\"," +
                     " \n  \""+ prompt + "\":" +
                     " [\n{ \n \"role\": \"user\"," +
                     "       \n \"content\": \"Hello\"\n}\n]\n}"
                    )
                    {
                        Headers = {ContentType = new MediaTypeHeaderValue("application/json")}
                    }
            };

            using (var response = await client.SendAsync(request))
            {
                if (response.ReasonPhrase == "Too Many Requests")  
                {
                    return(HandleBadRequest());
                }

                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
            }

            model = DecodeChatGptResponse(body);

            return model;
        }
    }
}
