﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YasShop.Domain.Users.RoleAgg.Entities;
using YasShop.Infrastructure.EfCore.Contracts;

namespace YasShop.Infrastructure.EfCore.Mapping.Users
{
    public class tblRoleConfiguration : IEntityTypeConfiguration<tblRoles>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblRoles> builder)
        {
            builder.Property(a => a.PageName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(250);
        }
    }
}
