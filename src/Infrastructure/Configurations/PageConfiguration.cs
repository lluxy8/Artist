using Core.Common.Constants;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class PageConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.ToTable("Pages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DisplayName)
                   .HasMaxLength(MaxLenghts.Small)
                   .IsRequired();

            builder.Property(x => x.UrlName)
                   .HasMaxLength(MaxLenghts.Small)
                   .IsRequired();

            builder.HasOne(p => p.PageContent)
                   .WithOne(pc => pc.Page)
                   .HasForeignKey<PageContent>(pc => pc.PageId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }

}
