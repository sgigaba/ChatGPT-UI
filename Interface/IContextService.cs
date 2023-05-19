namespace ChatGPT_UI.Interface
{
    using System.Collections.Generic;

    public interface IContextService<T>
        where T : class
    {
        public void SaveHistory(T model);

        public List<T> FetchHistory();
    }
}
