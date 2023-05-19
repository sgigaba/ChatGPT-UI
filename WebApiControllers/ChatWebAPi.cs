namespace ChatGPT_UI.WebApiControllers
{
    using DevExtreme.AspNet.Mvc;
    using DevExtreme.AspNet.Data;

    using Microsoft.AspNetCore.Mvc;

    using ChatGPT_UI.Models;
    using ChatGPT_UI.Interface;

    public class ChatWebApi : Controller
    {
        private readonly IChatApiService chatService;
        private readonly IChatContextService chatContextService;

        public ChatWebApi(IChatContextService contextService, IChatApiService chatService)
        {
            this.chatContextService = contextService;
            this.chatService = chatService;
        }

        [HttpGet]

        public IActionResult GetChatHistory(DataSourceLoadOptions loadOptions, string data, string AIModel)
        {
            var histories = chatContextService.FetchHistory();

            return Json(DataSourceLoader.Load(histories, loadOptions));
        }

        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions, string data, string AIModel)
        {
            var chatGPTResponse = new ChatGPTResponse();
            var model = await this.chatService.GetAPIResponse(data, AIModel);
            var chats = new List<Chats>();

            var chat = new Chats()
            {
                Id = 1,
                Message = model.choices[0].message.content,
                Prompt = data
            };

            chats.Add(chat);

            var chatHistory = new ChatHistory()
            {
                Content = model.choices[0].message.content,
                Prompt = data
            };

            chatContextService.SaveHistory(chatHistory);
            
            return Json(DataSourceLoader.Load(chats, loadOptions));
        }
    }
}
