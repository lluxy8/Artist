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
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DisplayName)
                   .HasMaxLength(MaxLenghts.Small)
                   .IsRequired();

            builder.Property(x => x.Description)
                   .HasMaxLength(MaxLenghts.XLarge);

            builder.HasOne(p => p.SubCategory)
                   .WithMany(c => c.Projects)
                   .HasForeignKey(p => p.SubCategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Images)
                   .WithOne(i => i.Project)
                   .HasForeignKey(i => i.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
