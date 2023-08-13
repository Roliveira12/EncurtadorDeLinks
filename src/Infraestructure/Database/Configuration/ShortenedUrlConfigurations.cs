using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration
{
    public class ShortenedUrlConfigurations : IEntityTypeConfiguration<ShortenedUrl>
    {
        public void Configure(EntityTypeBuilder<ShortenedUrl> builder)
        {
            builder.ToTable("shortenedurls");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.OriginalUrl).IsRequired().HasColumnName("OriginalUrl");
            builder.Property(x => x.ShorterUrlId).IsRequired().HasColumnName("ShorterUrl");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("NOW()");
        }
    }
}