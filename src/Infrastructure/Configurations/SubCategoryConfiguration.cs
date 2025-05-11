using Core.Common.Constants;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.ToTable("SubCategories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.UrlName)
                   .HasMaxLength(MaxLenghts.Small)
                   .IsRequired();

            builder.Property(x => x.DisplayName)
                   .HasMaxLength(MaxLenghts.Small)
                   .IsRequired();

            builder.Property(x => x.ImageUrl)
                   .HasMaxLength(MaxLenghts.XLarge)
                   .IsRequired();

            builder.HasMany(c => c.Projects)
                   .WithOne(p => p.SubCategory)
                   .HasForeignKey(p => p.SubCategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Category)
                     .WithMany(p => p.SubCategories)
                     .HasForeignKey(p => p.CategoryId)
                     .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.UrlName).IsUnique();
        }
    }
}
