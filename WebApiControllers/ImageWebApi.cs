namespace ChatGPT_UI.WebApiControllers
{
    using DevExtreme.AspNet.Data;
    using DevExtreme.AspNet.Mvc;

    using Microsoft.AspNetCore.Mvc;

    using ChatGPT_UI.Interface;
    using ChatGPT_UI.Models;

    public class ImageWebApi : Controller
    {
        private readonly IImageApiService imageService;
        private readonly IImageContextService imageContextService;

        public ImageWebApi(IImageApiService imageService, IImageContextService imageContextService)
        {
            this.imageService = imageService;
            this.imageContextService = imageContextService;
        }

        public IActionResult GetImageHistory(DataSourceLoadOptions loadOptions, string data, string AIModel)
        {
            var histories = imageContextService.FetchHistory();

            return Json(DataSourceLoader.Load(histories, loadOptions));
        }

        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions, string data, string AIModel)
        {
            var model = await this.imageService.GetAPIResponse(data, AIModel);

            var images = new List<Images>();

            var image = new Images()
            {
                Id = 1,
                prompt = data,
                url = model.data.First().url
            };

            var imageHistory = new ImageHistory()
            {
                Content = model.data.First().url,
                Prompt = data,
            };

            images.Add(image);
            imageContextService.SaveHistory(imageHistory);

            return Json(DataSourceLoader.Load(images, loadOptions));
        }
    }
}
