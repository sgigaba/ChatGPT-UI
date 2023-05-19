namespace ChatGPT_UI.WebApiControllers
{
    using ChatGPT_UI.Interface;
    using ChatGPT_UI.Models;
    using ChatGPT_UI.Services;
    using DevExtreme.AspNet.Data;
    using DevExtreme.AspNet.Mvc;
    using Microsoft.AspNetCore.Mvc;
    using static System.Net.WebRequestMethods;

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
               // url = "https://oaidalleapiprodscus.blob.core.windows.net/private/org-2FnMdmukLY7HKFjgYW6BOtyO/user-YURo22XwNWMJedCtFsb9PwBz/img-7Y9F227CvqujRr8pPOSHyGmO.png?st=2023-05-19T08%3A18%3A55Z&se=2023-05-19T10%3A18%3A55Z&sp=r&sv=2021-08-06&sr=b&rscd=inline&rsct=image/png&skoid=6aaadede-4fb3-4698-a8f6-684d7786b067&sktid=a48cca56-e6da-484e-a814-9c849652bcb3&skt=2023-05-19T08%3A53%3A21Z&ske=2023-05-20T08%3A53%3A21Z&sks=b&skv=2021-08-06&sig=NmLg8gyLqp2UtfBBZZiHCQ48QAJMu3%2BiFRsTdRyKfhk%3D"
            };

            var imageHistory = new ImageHistory()
            {
                Content = "https://oaidalleapiprodscus.blob.core.windows.net/private/org-2FnMdmukLY7HKFjgYW6BOtyO/user-YURo22XwNWMJedCtFsb9PwBz/img-7Y9F227CvqujRr8pPOSHyGmO.png?st=2023-05-19T08%3A18%3A55Z&se=2023-05-19T10%3A18%3A55Z&sp=r&sv=2021-08-06&sr=b&rscd=inline&rsct=image/png&skoid=6aaadede-4fb3-4698-a8f6-684d7786b067&sktid=a48cca56-e6da-484e-a814-9c849652bcb3&skt=2023-05-19T08%3A53%3A21Z&ske=2023-05-20T08%3A53%3A21Z&sks=b&skv=2021-08-06&sig=NmLg8gyLqp2UtfBBZZiHCQ48QAJMu3%2BiFRsTdRyKfhk%3D",
                Prompt = data,
            };

            images.Add(image);
            imageContextService.SaveHistory(imageHistory);

            return Json(DataSourceLoader.Load(images, loadOptions));
        }
    }
}
