namespace ChatGPT_UI.Models
{
    public class DallEResponse
    {
        public string created { get; set; }

        public List<Url> data { get; set; }
    }

    public class Url
    {
        public string url { get; set; }
    }
}
