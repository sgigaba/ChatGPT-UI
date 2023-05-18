namespace ChatGPT_UI.Models
{
    public class ChatGPTResponse
    {
        public string id { get; set; }

        public List<Choices> choices { get; set; }
    }

    public class Choices
    {
        public Message? message { get; set; }

        public string? text { get; set; }

    }

    public class Message
    {
        public string content { get; set; }
    }
}
