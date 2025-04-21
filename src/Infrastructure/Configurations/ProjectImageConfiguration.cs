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
    public class ProjectImageConfiguration : IEntityTypeConfiguration<ProjectImage>
    {
        public void Configure(EntityTypeBuilder<ProjectImage> builder)
        {
            builder.ToTable("ProjectImages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Url)
                   .HasMaxLength(MaxLenghts.XLarge)
                   .IsRequired();

            builder.HasOne(i => i.Project)
                   .WithMany(p => p.Images)
                   .HasForeignKey(i => i.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
