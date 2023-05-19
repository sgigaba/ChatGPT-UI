namespace ChatGPT_UI.Services
{
    using ChatGPT_UI.Interface;
    using ChatGPT_UI.Models;

    public class ImageContextService : ContextService<ImageHistory>, IImageContextService
    {
        public ImageContextService(ApplicationDbContext context) : base (context)
        {
        }
    }
}
