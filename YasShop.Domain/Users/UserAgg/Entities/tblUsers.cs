using Framework.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using YasShop.Domain.Users.AccessLevelAgg.Entities;

namespace YasShop.Domain.Users.UserAgg.Entities
{
    public class tblUsers : IdentityUser<Guid>, IEntity
    {
        public Guid AccessLevelId { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public DateTime Date { get; set; }
        public string OTPData { get; set; }

        public virtual tblAccessLevel tblAccessLevel { get; set; }
        public virtual ICollection<tblUserRole> tblUserRole { get; set; }

    }
}
