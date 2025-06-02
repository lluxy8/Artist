using System.Security.Cryptography.X509Certificates;
using Core.Common.Constants;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations
{
    public class PageContentConfiguration : IEntityTypeConfiguration<PageContent>
    {
        public void Configure(EntityTypeBuilder<PageContent> builder)
        {
            builder.ToTable("PageContents");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).HasMaxLength(MaxLenghts.Small);
            builder.Property(x => x.Description).HasMaxLength(MaxLenghts.Medium);

            builder.HasOne(pc => pc.Page)
                   .WithOne(p => p.PageContent)
                   .HasForeignKey<PageContent>(pc => pc.PageId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pc => pc.PageCarousels)
                   .WithOne(pc => pc.PageContent)
                   .HasForeignKey(pc => pc.PageContentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.PageSections)
                .WithOne(ps => ps.PageContent)
                .HasForeignKey(ps => ps.PageContentId)
                .OnDelete(DeleteBehavior.Cascade);
                
        }
    }

}
