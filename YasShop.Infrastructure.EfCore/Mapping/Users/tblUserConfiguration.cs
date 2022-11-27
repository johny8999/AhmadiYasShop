using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YasShop.Domain.Users.UserAgg.Entities;
using YasShop.Infrastructure.EfCore.Contracts;

namespace YasShop.Infrastructure.EfCore.Mapping.Users
{
    public class tblUserConfiguration : IEntityTypeConfiguration<tblUsers>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblUsers> builder)
        {
            builder.Property(conf => conf.FullName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.AccessLevelId).HasMaxLength(450);//foreignkey
            builder.Property(a => a.OTPData).HasMaxLength(3000);

            builder.HasOne(a => a.tblAccessLevel)
                    .WithMany(a => a.tblUsers)
                    .HasPrincipalKey(a => a.Id)
                    .HasForeignKey(a => a.AccessLevelId)
                    .OnDelete(DeleteBehavior.Restrict);
                
            
        }
    }
}
