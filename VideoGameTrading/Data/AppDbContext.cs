using VideoGameTrading.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VideoGameTrading.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Item>? Items { get; set; }

        public DbSet<ShopLength>? ShopLength { get; set; }

        public DbSet<CartLength>? CartLength { get; set; }
    }
}
