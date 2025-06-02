using Core.Common.Constants;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class PageSectionConfiguration : IEntityTypeConfiguration<PageSection>
    {
        public void Configure(EntityTypeBuilder<PageSection> builder)
        {
            builder.ToTable("PageSections");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Text1).HasMaxLength(MaxLenghts.Medium);
            builder.Property(x => x.Text2).HasMaxLength(MaxLenghts.Medium);
            builder.Property(x => x.Text3).HasMaxLength(MaxLenghts.Large);
            builder.Property(x => x.ImageUrl).HasMaxLength(MaxLenghts.XLarge);

            builder.HasOne(x => x.PageContent)
                .WithMany(pc => pc.PageSections)
                .HasForeignKey(x => x.PageContentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
