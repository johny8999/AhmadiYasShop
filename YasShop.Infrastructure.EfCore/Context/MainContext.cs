using Framework.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using YasShop.Domain.Users.RoleAgg.Entities;
using YasShop.Domain.Users.UserAgg.Entities;
using YasShop.Infrastructure.EfCore.Common.ExMethod;
using YasShop.Infrastructure.EfCore.Config;
using YasShop.Infrastructure.EfCore.Contracts;

namespace YasShop.Infrastructure.EfCore.Context
{
    public class MainContext : IdentityDbContext<tblUsers, tblRoles, Guid, IdentityUserClaim<Guid>,
                                tblUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            //builder.ApplyConfiguration(new tblUserConfiguration());

            var EntitiesAssambly = typeof(IEntity).Assembly;
            builder.RegisterAllEntities<IEntity>(EntitiesAssambly);

            var EntitiesConfAssambly = typeof(IEntityConf).Assembly;
            builder.RegisterEntityTypeConfiguration(EntitiesConfAssambly);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.Get());
        }


    }
}

