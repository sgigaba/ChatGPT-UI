namespace ChatGPT_UI.WebApiControllers
{
    using DevExtreme.AspNet.Mvc;
    using DevExtreme.AspNet.Data;

    using Microsoft.AspNetCore.Mvc;

    using ChatGPT_UI.Models;
    using ChatGPT_UI.Services;

    public class ChatWebApi : Controller
    {
        private readonly ApiService apiService;

        public ChatWebApi(ApiService apiService) 
        {
            this.apiService = apiService;
        }

        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            this.apiService.GetChatGptResponse();

            var chats = new List<Chat>();

            var chat = new Chat()
            {
                Id = 1,
                Message = "hello"
            };

            chats.Add(chat);

            return Json(DataSourceLoader.Load(chats, loadOptions));
        }
    }
}
