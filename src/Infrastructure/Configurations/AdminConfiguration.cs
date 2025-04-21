using Core.Common.Constants;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admins");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .HasMaxLength(MaxLenghts.Small)
                   .IsRequired();

            builder.Property(x => x.PasswordHash)
                   .HasMaxLength(MaxLenghts.XLarge)
                   .IsRequired();

            builder.HasMany(a => a.LoginAttempts)
                   .WithOne(la => la.Admin)
                   .HasForeignKey(la => la.AdminId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
