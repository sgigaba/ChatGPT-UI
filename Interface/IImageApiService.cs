namespace ChatGPT_UI.Interface
{
    using ChatGPT_UI.Models;

    public interface IImageApiService : IApiService<Images>
    {
        public new Task<DallEResponse> GetAPIResponse(string prompt, string AImodel);

        public DallEResponse DecodeDallEReponse(string body);
    }
}
