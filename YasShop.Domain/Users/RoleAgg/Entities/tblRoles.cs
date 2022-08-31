using Framework.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using YasShop.Domain.Users.AccessLevelAgg.Entities;
using YasShop.Domain.Users.UserAgg.Entities;

namespace YasShop.Domain.Users.RoleAgg.Entities;

public class tblRoles : IdentityRole<Guid>, IEntity
{
    public Guid? ParentId { get; set; }
    public string PageName { get; set; }
    public int Sort { get; set; }
    public string Description { get; set; }


    public virtual ICollection<tblAccessLevelRoles> tblAccessLevelRoles { get; set; }

    public virtual ICollection<tblRoles> tblRolesChilds { get; set; }
    public virtual ICollection<tblUserRole> tblUserRoles { get; set; }
    public virtual tblRoles tblRoleParent { get; set; }
}
