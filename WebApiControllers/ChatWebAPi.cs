namespace ChatGPT_UI.WebApiControllers
{
    using DevExtreme.AspNet.Mvc;
    using DevExtreme.AspNet.Data;

    using Microsoft.AspNetCore.Mvc;

    using ChatGPT_UI.Models;
    using ChatGPT_UI.Services;
    using ChatGPT_UI.Interface;

    public class ChatWebApi : Controller
    {
        /*private readonly IApiService apiService;*/
        private readonly IChatService chatService;
        private readonly ContextService contextService;

        public ChatWebApi(ContextService contextService, IChatService chatService)
        {
           // this.apiService = apiService;
            this.contextService = contextService;
            this.chatService = chatService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions, string data, string AIModel)
        {
            var chatGPTResponse = new ChatGPTResponse();
            var model = await this.chatService.GetAPIResponse(data, AIModel);
           // var model = this.apiService.DummyRequest();
            var chats = new List<Chats>();

            var chat = new Chats()
            {
                Id = 1,
                Message = model.choices[0].message.content,
                Prompt = data
            };

            chats.Add(chat);
            contextService.SaveChat(chat);
            
            return Json(DataSourceLoader.Load(chats, loadOptions));
        }
    }
}
