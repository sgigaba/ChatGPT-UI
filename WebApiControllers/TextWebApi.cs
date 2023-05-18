namespace ChatGPT_UI.WebApiControllers
{
    using ChatGPT_UI.Interface;
    using ChatGPT_UI.Models;
    using ChatGPT_UI.Services;
    using DevExtreme.AspNet.Data;
    using DevExtreme.AspNet.Mvc;
    using Microsoft.AspNetCore.Mvc;
    using System.Xml.Linq;

    public class TextWebApi : Controller
    {
        private readonly ITextService textApiService;
        private readonly ContextService contextService;

        public TextWebApi(ITextService textApiService, ContextService contextService)
        {
            this.textApiService = textApiService;
            this.contextService = contextService;
        }

        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions, string data, string AIModel)
        {
            var chatGPTResponse = new ChatGPTResponse();
            var model = await this.textApiService.GetAPIResponse(data, AIModel);

            var texts = new List<Texts>();

            var text = new Texts()
            {
                Id = 1,
                Message = model.choices[0].text,
                Prompt = data
            };

            texts.Add(text);
           // contextService.SaveChat(text);

            return Json(DataSourceLoader.Load(texts, loadOptions));
        }
    }
}
