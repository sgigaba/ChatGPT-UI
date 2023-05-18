namespace ChatGPT_UI.Services
{
    using System.Diagnostics.Metrics;
    using System.Net.Http.Headers;
    using System.Net.Mime;
    using System.Reflection.PortableExecutable;
    using System.Runtime.CompilerServices;
    using System.Text;
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

        public StringContent GetContentString(string prompt, string AImodel)
        {
            var content = new StringContent("");

            if (AImodel == "text-davinci-003")
            {
                content = new StringContent("{\n" +
                    "\"model\": \"text-davinci-003\",\n" +
                    "\"prompt\": \"Say this is a test\",\n" +
                    "\"max_tokens\": 7,\n" +
                    "\"temprature\": 0,\n" +
                    "\"top_p\": 1,\n" +
                    "\"n\": 1,\n" +
                    "\"stream\": false,\n" +
                    "\"logprobs\":null,\n" +
                    "\"stop\": \"\"\n]");
            }
            else
            {
                content = new StringContent("{\n" +
               "\"model\":\"" + AImodel + "\", \n" +
               "\"messages\": [\n" +
               "{\n" +
               "\"role\": \"user\",\n" +
               "\"content\": \"" + prompt + "\"\n" +
               "}\n" +
               "]\n" +
               "}");
            }

            return content;
        }

        public async Task<ChatGPTResponse> GetAPIResponse(string prompt, string AImodel)
        {
            var client = httpClientFactory.CreateClient();
            var body = "";
            var model = new ChatGPTResponse();


            var content = GetContentString(prompt, AImodel);
            if (AImodel == "text-davinci-003")
            {

            }
            var uri = "https://api.openai.com/v1/completions";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(uri),
                Headers =
                {
                    { "Authorization", "Bearer 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" },
                },

                Content = new StringContent("")
                {
                    Headers = {ContentType = new MediaTypeHeaderValue("application/json")}
                }
            };

            request.Content = content;
         
            using (var response = await client.SendAsync(request))
            {
                if (response.ReasonPhrase == "Too Many Requests")  
                {
                    return(HandleBadRequest());
                }

                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
            }

            model = DecodeAPIReponse(body);

            return model;
        }
    }
}


