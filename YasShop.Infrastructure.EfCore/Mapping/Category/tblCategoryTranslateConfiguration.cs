using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YasShop.Domain.Category.Entities;
using YasShop.Infrastructure.EfCore.Contracts;

namespace YasShop.Infrastructure.EfCore.Mapping.Category
{
    public class tblCategoryTranslateConfiguration : IEntityTypeConfiguration<tblCategoryTranslates>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblCategoryTranslates> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.CategoryId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.LangId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ImgId).IsRequired(false).HasMaxLength(150);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Description).IsRequired(false);
        }
    }
}
