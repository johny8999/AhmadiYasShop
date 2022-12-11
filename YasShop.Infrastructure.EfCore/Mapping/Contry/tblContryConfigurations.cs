using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YasShop.Domain.FileManager.FilesAgg.Entities;
using YasShop.Domain.Region.CountryAgg.Entity;
using YasShop.Infrastructure.EfCore.Contracts;

namespace YasShop.Infrastructure.EfCore.Mapping.Contry
{
    public class tblContryConfigurations : IEntityTypeConfiguration<tblContries>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblContries> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.FlagImgId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(150);
            builder.Property(a => a.PhoneCode).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblFiles)
                   .WithOne(a => a.tblContries)
                   .HasPrincipalKey<tblFiles>(a => a.Id)
                   .HasForeignKey<tblContries>(a => a.FlagImgId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
