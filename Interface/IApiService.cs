namespace ChatGPT_UI.Interface
{
    using ChatGPT_UI.Models;

    public interface IApiService<T>
        where T : class
    {
        public ChatGPTResponse DecodeAPIReponse(string body);

        public ChatGPTResponse HandleBadRequest();

        public ChatGPTResponse DummyRequest();

        public Task<ChatGPTResponse> GetAPIResponse(string prompt, string AImodel);
    }
}
