using Core.Common.Constants;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class SocialConfiguration : IEntityTypeConfiguration<Social>
    {
        public void Configure(EntityTypeBuilder<Social> builder)
        {
            builder.ToTable("Socials");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(MaxLenghts.Small)
                .IsRequired();

            builder.Property(x => x.Url)
                .HasMaxLength(MaxLenghts.Large)
                .IsRequired();

            builder.Property(x => x.IconUrl)
                .HasMaxLength(MaxLenghts.XLarge)
                .IsRequired();
        }
    }

}
