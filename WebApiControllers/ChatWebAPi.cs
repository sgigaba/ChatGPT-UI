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

        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            var model = await this.apiService.GetChatGptResponse();

            var chats = new List<Chat>();

            var chat = new Chat()
            {
                Id = 1,
                Message = model.choices[0].message.content,
            };

            chats.Add(chat);

            return Json(DataSourceLoader.Load(chats, loadOptions));
        }
    }
}
