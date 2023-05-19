namespace ChatGPT_UI.Services
{
    using ChatGPT_UI.Interface;

    using System.Collections.Generic;

    public class ContextService<T> : IContextService<T>
        where T : class
    {
        private readonly ApplicationDbContext context;

        public ContextService(ApplicationDbContext context) 
        {
            this.context = context;
        }

        public void SaveHistory(T model)
        {
            context.Set<T>().Add(model);
            context.SaveChanges();
        }

        public List<T> FetchHistory()
        {
            return this.context.Set<T>().ToList();
        }
    }
}
