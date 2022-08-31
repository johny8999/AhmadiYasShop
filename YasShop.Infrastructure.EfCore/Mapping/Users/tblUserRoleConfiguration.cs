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

        }
    }

}
