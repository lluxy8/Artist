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
    public class PageContentConfiguration : IEntityTypeConfiguration<PageContent>
    {
        public void Configure(EntityTypeBuilder<PageContent> builder)
        {
            builder.ToTable("PageContents");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ImageUrl)
                   .HasMaxLength(MaxLenghts.XLarge);

            builder.Property(x => x.Text1)
                   .HasMaxLength(MaxLenghts.Large);

            builder.Property(x => x.Text2)
                   .HasMaxLength(MaxLenghts.Large);

            builder.Property(x => x.Text3)
                   .HasMaxLength(MaxLenghts.Large);

            builder.HasOne(pc => pc.Page)
                   .WithOne(p => p.PageContent)
                   .HasForeignKey<PageContent>(pc => pc.PageId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pc => pc.PageCarousels)
                   .WithOne(pc => pc.PageContent)
                   .HasForeignKey(pc => pc.PageContentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
