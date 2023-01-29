using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YasShop.Domain.Users.AccessLevelAgg.Entities;
using YasShop.Infrastructure.EfCore.Contracts;

namespace YasShop.Infrastructure.EfCore.Mapping.AccessLevels
{
    public class tblAccessLevelConfiguration : IEntityTypeConfiguration<tblAccessLevel>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblAccessLevel> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(450);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            
        }
    }
}
