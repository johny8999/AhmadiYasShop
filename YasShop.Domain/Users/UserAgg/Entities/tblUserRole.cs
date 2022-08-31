using Framework.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using YasShop.Domain.Users.RoleAgg.Entities;

namespace YasShop.Domain.Users.UserAgg.Entities
{
    public class tblUserRole : IdentityUserRole<Guid>, IEntity
    {
        public Guid Id { get; set; }

        public virtual tblUsers tblUser { get; set; }
        public virtual tblRoles tblRole { get; set; }


    }
}
