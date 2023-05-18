using Microsoft.AspNetCore.Mvc;

namespace ChatGPT_UI.Controllers
{
    public class TextController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
