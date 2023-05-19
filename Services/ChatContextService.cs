namespace ChatGPT_UI.Services
{
    using ChatGPT_UI.Interface;
    using ChatGPT_UI.Models;

    public class ChatContextService : ContextService<ChatHistory>, IChatContextService
    {
        public ChatContextService(ApplicationDbContext context) 
            : base(context)
        {
        }
    }
}
