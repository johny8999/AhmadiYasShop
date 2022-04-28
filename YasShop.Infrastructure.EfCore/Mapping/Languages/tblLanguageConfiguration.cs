using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YasShop.Domain.Region.LanguageAgg.Entities;
using YasShop.Infrastructure.EfCore.Contracts;
using YasShop.Infrastructure.EfCore.Seed;

namespace YasShop.Infrastructure.EfCore.Mapping.Languages
{
    public class tblLanguageConfiguration : IEntityTypeConfiguration<tblLanguages>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblLanguages> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(450);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Code).IsRequired().HasMaxLength(50);
            builder.Property(a => a.Abbr).IsRequired().HasMaxLength(10);
            builder.Property(a => a.NativeName).IsRequired().HasMaxLength(150);

            //new SeedLanguages().ApplySeed(builder);

        }
    }
}
