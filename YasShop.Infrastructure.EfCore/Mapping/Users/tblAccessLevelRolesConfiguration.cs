using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YasShop.Domain.Users.AccessLevelAgg.Entities;
using YasShop.Infrastructure.EfCore.Contracts;

namespace YasShop.Infrastructure.EfCore.Mapping.Users
{
    public class tblAccessLevelRolesConfiguration : IEntityTypeConfiguration<tblAccessLevelRoles>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblAccessLevelRoles> builder)
        {
            builder.HasKey(conf => conf.Id);
            builder.Property(conf => conf.Id).IsRequired().HasMaxLength(150);
            builder.Property(conf => conf.RoleId).IsRequired().HasMaxLength(150);
            builder.Property(conf => conf.AccessLevelId).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblRole)
                .WithMany(a => a.tblAccessLevelRoles)
                .HasPrincipalKey(a => a.Id)
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblAccessLevel)
                .WithMany(a => a.tblAccessLevelRoles)
                .HasPrincipalKey(a => a.Id)
                .HasForeignKey(a => a.AccessLevelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
