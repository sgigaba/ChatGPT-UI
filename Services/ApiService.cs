namespace ChatGPT_UI.Services
{
    using System.Net.Http.Headers;

    using ChatGPT_UI.Interface;
    using ChatGPT_UI.Models;

    using Newtonsoft.Json;

    public class ApiService<T> : IApiService<T> 
        where T : class
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public ChatGPTResponse DecodeAPIReponse(string body)
        {
            var model = JsonConvert.DeserializeObject<ChatGPTResponse>(body);
            
            return model;
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

        public ChatGPTResponse DummyRequest()
        {
            var choices = new List<Choices>();

            var choice = new Choices()
            {
                message = new Message()
                {
                    content = "This data is from the dummy model :D"
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

       
        public async Task<ChatGPTResponse> GetAPIResponse(string prompt, string AImodel)
        {
            return null;
        }
    }
}


