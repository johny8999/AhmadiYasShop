using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using YasShop.Infrastructure.EfCore.Contracts;
using YasShop.Domain.ProductAgg.Entity;

namespace YasShop.Infrastructure.EfCore.Mapping.Products
{
    public class tblProductsConfigurations : IEntityTypeConfiguration<tblProducts>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProducts> builder)
        {
            builder.HasKey(conf => conf.Id);
            builder.Property(conf => conf.Id).IsRequired().HasMaxLength(150);
            builder.Property(conf => conf.TopicId).IsRequired(false).HasMaxLength(150);
            builder.Property(conf => conf.AuthorUserId).IsRequired(false).HasMaxLength(450);
            builder.Property(conf => conf.CategoryId).IsRequired(false).HasMaxLength(150);
            builder.Property(conf => conf.ProductGroupId).IsRequired(false).HasMaxLength(150);
            builder.Property(conf => conf.LangId).IsRequired(false).HasMaxLength(150);
            builder.Property(conf => conf.UniqueNumber).IsRequired(false).HasMaxLength(100);
            builder.Property(conf => conf.Name).IsRequired(false).HasMaxLength(100);
            builder.Property(conf => conf.Title).IsRequired(false).HasMaxLength(100);
            builder.Property(conf => conf.Description).IsRequired(false);
            builder.Property(conf => conf.Title).IsRequired(false).HasMaxLength(100);
            builder.Property(conf => conf.MetaTagKeyword).IsRequired(false).HasMaxLength(200);
            builder.Property(conf => conf.MetaTagCanonical).IsRequired(false).HasMaxLength(200);
            builder.Property(conf => conf.Description).IsRequired(false).HasMaxLength(200);
            builder.Property(conf => conf.IncompleteReason).IsRequired(false).HasMaxLength(500);

            builder.HasOne(a => a.tblLanguages)
                  .WithMany(a => a.tblProducts)
                  .HasPrincipalKey(a => a.Id)
                  .HasForeignKey(a => a.LangId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblTopics)
                  .WithMany(a => a.tblProducts)
                  .HasPrincipalKey(a => a.Id)
                  .HasForeignKey(a => a.TopicId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
