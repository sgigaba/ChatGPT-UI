namespace ChatGPT_UI.WebApiControllers
{
    using DevExtreme.AspNet.Mvc;
    using DevExtreme.AspNet.Data;

    using Microsoft.AspNetCore.Mvc;

    using ChatGPT_UI.Models;

    public class ChatWebApi : Controller
    {
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
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
