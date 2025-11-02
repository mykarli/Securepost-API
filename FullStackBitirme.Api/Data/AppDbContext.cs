using Microsoft.EntityFrameworkCore;
using FullStackBitirme.Api.Models;

namespace FullStackBitirme.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public AppDbContext() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Migration sırasında EF'nin database bağlantısını tanıması için
                optionsBuilder.UseSqlite("Data Source=app.db");
            }
        }
    }
}
