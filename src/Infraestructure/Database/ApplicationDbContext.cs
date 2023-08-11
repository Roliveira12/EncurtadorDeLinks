using Domain.Entities;
using Infrastructure.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ShortenedUrl> ShortUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ShortenedUrlConfigurations());
        }
    }
}