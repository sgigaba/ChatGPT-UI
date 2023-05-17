namespace ChatGPT_UI.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ChatHistory
    {
        [Key]
        public int Id { get; set; }

        public string? Content { get; set; }

        public string? Prompt { get; set; }
    }
}
