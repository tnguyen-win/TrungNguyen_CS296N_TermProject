using VideoGameTrading.Models;
using Microsoft.EntityFrameworkCore;

namespace VideoGameTrading.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }

        public DbSet<AppUser> Users { get; set; }
    }
}
