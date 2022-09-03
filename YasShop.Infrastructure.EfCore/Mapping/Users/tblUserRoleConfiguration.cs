using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YasShop.Domain.Users.UserAgg.Entities;
using YasShop.Infrastructure.EfCore.Contracts;

namespace YasShop.Infrastructure.EfCore.Mapping.Users
{
    public class tblUserRoleConfiguration : IEntityTypeConfiguration<tblUserRole>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblUserRole> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.RoleId).HasMaxLength(450).IsRequired();
            builder.Property(a => a.UserId).HasMaxLength(450).IsRequired();

            builder.HasOne(a => a.tblRole)
                .WithMany(a => a.tblUserRoles)
                .HasPrincipalKey(a => a.Id)
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblUser)
                .WithMany(a => a.tblUserRole)
                .HasPrincipalKey(a => a.Id)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
