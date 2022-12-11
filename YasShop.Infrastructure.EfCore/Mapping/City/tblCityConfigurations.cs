using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YasShop.Domain.Region.CityAgg.Entity;
using YasShop.Infrastructure.EfCore.Contracts;

namespace YasShop.Infrastructure.EfCore.Mapping.City
{
    public class tblCityConfigurations : IEntityTypeConfiguration<tblCities>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblCities> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProvinceId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblProvince)
                   .WithMany(a => a.tblCities)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProvinceId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}