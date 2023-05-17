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

        public async Task<ChatGPTResponse> GetChatGptResponse(string prompt)
        {
            var client = httpClientFactory.CreateClient();
            var model = new ChatGPTResponse();
            var body = "";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://openai80.p.rapidapi.com/chat/completions"),
                Headers =
                {
                    { "X-RapidAPI-Key", "528a6b647bmsh85c5e35f1d7746ep163566jsn7e1de44f25e9" },
                    { "X-RapidAPI-Host", "openai80.p.rapidapi.com" },
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
                    var choices = new List<Choices>();

                    var choice = new Choices()
                    {
                        message = new Message()
                        {
                            content = "ChatGPT has too many requests at the moment. Please try again soon"
                        }
                    };

                    choices.Add(choice);

                    model = new ChatGPTResponse()
                    {
                        id = "null",
                        choices = choices

                    };

                    return model;
                }

                response.EnsureSuccessStatusCode();

                body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }

            model = DecodeChatGptResponse(body);

            return model;
        }
    }
}
