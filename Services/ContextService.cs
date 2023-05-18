using ChatGPT_UI.Models;

namespace ChatGPT_UI.Services
{
    public class ContextService
    {
        private readonly ApplicationDbContext context;

        public ContextService(ApplicationDbContext context) 
        {
            this.context = context;
        }

        public void SaveChat(Chats model)
        {
            var ChatHistory = new ChatHistory()
            {
                Content = model.Message,
                Prompt = model.Prompt,
            };

            context.ChatHistory.Add(ChatHistory);
            context.SaveChanges();
        }
    }
}
