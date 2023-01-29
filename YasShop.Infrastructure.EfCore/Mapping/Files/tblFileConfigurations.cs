using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using YasShop.Infrastructure.EfCore.Contracts;
using YasShop.Domain.FileManager.FilesAgg.Entities;

namespace YasShop.Infrastructure.EfCore.Mapping.Files
{
    public class tblFileConfigurations : IEntityTypeConfiguration<tblFiles>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblFiles> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.FileTypeId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.FilePathId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.UserId).IsRequired(false).HasMaxLength(450);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(100);
            builder.Property(a => a.FileName).IsRequired().HasMaxLength(100);

            builder.HasOne(a => a.tblFilePath)
                   .WithMany(a => a.tblFiles)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.FilePathId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblFileTypes)
                   .WithMany(a => a.tblFiles)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.FileTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
           
        }
    }
}
