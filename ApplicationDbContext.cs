namespace ChatGPT_UI
{
    using ChatGPT_UI.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<ChatHistory> ChatHistory { get; set; }

        public DbSet<TextHistory> TextHistory { get; set; }

        public DbSet<ImageHistory> ImageHistory { get; set; }
    }
}
