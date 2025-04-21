using Core.Common.Constants;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class PageCarouselConfiguration : IEntityTypeConfiguration<PageCarousel>
    {
        public void Configure(EntityTypeBuilder<PageCarousel> builder)
        {
            builder.Property(x => x.ImageUrl)
                   .HasMaxLength(MaxLenghts.XLarge);

            builder.Property(x => x.Text1)
                   .HasMaxLength(MaxLenghts.Medium);

            builder.Property(x => x.Text2)
                   .HasMaxLength(MaxLenghts.Medium);

            builder.Property(x => x.Text3)
                   .HasMaxLength(MaxLenghts.Medium);

            builder.HasOne(pc => pc.PageContent)
                   .WithMany(pc => pc.PageCarousels)
                   .HasForeignKey(pc => pc.PageContentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
