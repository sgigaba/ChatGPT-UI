namespace ChatGPT_UI.WebApiControllers
{
    using ChatGPT_UI.Interface;
    using ChatGPT_UI.Models;
    using DevExtreme.AspNet.Data;
    using DevExtreme.AspNet.Mvc;
    using Microsoft.AspNetCore.Mvc;

    public class ImageWebApi : Controller
    {
        private readonly IImageService imageService;

        public ImageWebApi(IImageService imageService)
        {
            this.imageService = imageService;
        }

        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions, string data, string AIModel)
        {
            var model = await this.imageService.GetAPIResponse(data, AIModel);

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
