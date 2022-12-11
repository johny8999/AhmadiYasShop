using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YasShop.Domain.FileManager.FileTypes.Entity;
using YasShop.Infrastructure.EfCore.Contracts;

namespace YasShop.Infrastructure.EfCore.Mapping.Files
{
    public class tblFileTypeConfigurations : IEntityTypeConfiguration<tblFileTypes>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblFileTypes> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.MimeType).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Extentions).IsRequired().HasMaxLength(100);
        }
    }
}
