using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YasShop.Domain.Region.ProvinceAgg.Entity;
using YasShop.Infrastructure.EfCore.Contracts;

namespace YasShop.Infrastructure.EfCore.Mapping.Province
{
    public class tblProvinceConfigurations : IEntityTypeConfiguration<tblProvince>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProvince> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.CountryId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblContries)
                   .WithMany(a => a.tblProvince)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
