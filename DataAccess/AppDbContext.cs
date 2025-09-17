
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {  }

        public DbSet<Entity> Anime { get; set; }

    }
}
