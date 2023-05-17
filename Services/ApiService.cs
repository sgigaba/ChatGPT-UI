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

        public async Task<ChatGPTResponse> GetChatGptResponse()
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
                    { "X-RapidAPI-Key", "0fadb49b35msh730b53ec9cd84b3p13ae59jsn3256bf1764cf" },
                    { "X-RapidAPI-Host", "openai80.p.rapidapi.com" },
                },
                Content = new StringContent
                    ("{\n \"model\": \"gpt-3.5-turbo\"," +
                     " \n  \"messages\":" +
                     " [\n{ \n \"role\": \"user\"," +
                     "       \n \"content\": \"Hello\"\n}\n]\n}"
                    )
                    {
                        Headers = {ContentType = new MediaTypeHeaderValue("application/json")}
                    }
            };

            using (var response = await client.SendAsync(request))
            {
	            response.EnsureSuccessStatusCode();
	            body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }

            model = DecodeChatGptResponse(body);

            return model;
        }
    }
}
