using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YasShop.Domain.FileManager.FileServers.Entities;
using YasShop.Infrastructure.EfCore.Contracts;
using YasShop.Domain.FileManager.FilePath.Entities;

namespace YasShop.Infrastructure.EfCore.Mapping.Files
{
    public class tblFilePathConfigurations : IEntityTypeConfiguration<tblFilePath>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblFilePath> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.FileServerId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Path).IsRequired().HasMaxLength(300);

            builder.HasOne(a => a.tblFileServer)
                   .WithMany(a => a.tblFilePath)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.FileServerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
