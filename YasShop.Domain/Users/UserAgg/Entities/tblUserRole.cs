using Framework.Domain;
using Microsoft.AspNetCore.Identity;
using System;

namespace YasShop.Domain.Users.UserAgg.Entities
{
    public class tblUserRole : IdentityUserRole<Guid>, IEntity
    {
        public Guid Id { get; set; }

    }
}
