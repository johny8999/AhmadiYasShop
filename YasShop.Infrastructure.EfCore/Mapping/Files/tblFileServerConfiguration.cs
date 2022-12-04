using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YasShop.Domain.FileManager.FileServers.Entities;
using YasShop.Infrastructure.EfCore.Contracts;

namespace YasShop.Infrastructure.EfCore.Mapping.Files
{
    public class tblFileServerConfiguration : IEntityTypeConfiguration<tblFileServer>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblFileServer> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Description).IsRequired().HasMaxLength(500);
            builder.Property(a => a.HttpDomain).IsRequired().HasMaxLength(100);
            builder.Property(a => a.HttpPath).IsRequired().HasMaxLength(100);
            builder.Property(a => a.FtpData).IsRequired();
        }
    }
