using Framework.Domain;
using System;
using YasShop.Domain.Users.RoleAgg.Entities;

namespace YasShop.Domain.Users.AccessLevelAgg.Entities
{
    public class tblAccessLevelRoles : IEntity
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid AccessLevelId { get; set; }

        public tblRoles tblRole { get; set; }
        public tblAccessLevel tblAccessLevel { get; set; }
    }
}
