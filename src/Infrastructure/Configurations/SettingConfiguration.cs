using Core.Common.Constants;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.ToTable("Settings");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CompanyName).HasMaxLength(MaxLenghts.XSmall);
            builder.Property(x => x.Address).HasMaxLength(MaxLenghts.Small);
            builder.Property(x => x.AddressGoogleMaps).HasMaxLength(MaxLenghts.XLarge);
            builder.Property(x => x.Email).HasMaxLength(MaxLenghts.Small);
            builder.Property(x => x.ExperienceYear).HasMaxLength(MaxLenghts.Medium);
            builder.Property(x => x.DoneCustomerCount).HasMaxLength(MaxLenghts.Small);
            builder.Property(x => x.DoneProjectsCount).HasMaxLength(MaxLenghts.Small);
            builder.Property(x => x.LogoUrl).HasMaxLength(MaxLenghts.XLarge);
            builder.Property(x => x.PhoneNumber).HasMaxLength(MaxLenghts.XSmall);
            builder.Property(x => x.PrimaryColor).HasMaxLength(MaxLenghts.HexCode);
            builder.Property(x => x.SecondaryColor).HasMaxLength(MaxLenghts.HexCode);
            builder.Property(x => x.ThirdColor).HasMaxLength(MaxLenghts.HexCode);
        }
    }
}
