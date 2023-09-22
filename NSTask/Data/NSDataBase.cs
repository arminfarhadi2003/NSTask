using Microsoft.EntityFrameworkCore;
using NSTask.Models.Entities;

namespace NSTask.Data
{
    public class NSDataBase:DbContext
    {
        public NSDataBase(DbContextOptions<NSDataBase> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
    }
}
