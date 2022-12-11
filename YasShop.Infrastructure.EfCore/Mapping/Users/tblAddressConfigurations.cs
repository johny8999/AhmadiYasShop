using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YasShop.Domain.Users.AddressAgg.Entity;
using YasShop.Infrastructure.EfCore.Contracts;

namespace YasShop.Infrastructure.EfCore.Mapping.Users
{
    public class tblAddressConfigurations : IEntityTypeConfiguration<tblAddress>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblAddress> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.UserId).IsRequired().HasMaxLength(450);
            builder.Property(a => a.CityId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.District).IsRequired(false).HasMaxLength(100);
            builder.Property(a => a.Address).IsRequired(false).HasMaxLength(200);
            builder.Property(a => a.Plaque).IsRequired(false).HasMaxLength(50);
            builder.Property(a => a.Unit).IsRequired(false).HasMaxLength(50);
            builder.Property(a => a.PostalCode).IsRequired().HasMaxLength(50);
            builder.Property(a => a.NationalCode).IsRequired().HasMaxLength(50);
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.PhoneNumber).IsRequired().HasMaxLength(50);

            builder.HasOne(a => a.tblUsers)
                   .WithMany(a => a.tblAddress)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblCities)
                   .WithMany(a => a.tblAddress)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CityId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}