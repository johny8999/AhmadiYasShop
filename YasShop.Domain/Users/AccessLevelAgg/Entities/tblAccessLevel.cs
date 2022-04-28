using Framework.Domain;
using System;
using System.Collections.Generic;
using YasShop.Domain.Users.UserAgg.Entities;

namespace YasShop.Domain.Users.AccessLevelAgg.Entities
{
    public class tblAccessLevel : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        public virtual ICollection<tblUsers> tblUsers { get; set; }
        public virtual ICollection<tblAccessLevelRoles> tblAccessLevelRoles { get; set; }
    }
}
