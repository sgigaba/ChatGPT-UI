using Microsoft.AspNetCore.Mvc;

namespace ChatGPT_UI.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
