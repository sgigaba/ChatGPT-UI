namespace ChatGPT_UI.Models
{
    public class ChatGPTResponse
    {
        public string id { get; set; }

        public List<Choices> choices { get; set; }
    }
}
