using Microsoft.EntityFrameworkCore;

namespace TimePlanner.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Entry> Entries { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
