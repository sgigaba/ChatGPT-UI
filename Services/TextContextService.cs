using ChatGPT_UI.Interface;
using ChatGPT_UI.Models;

namespace ChatGPT_UI.Services
{
    public class TextContextService : ContextService<TextHistory>, ITextContextService
    {
        public TextContextService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
