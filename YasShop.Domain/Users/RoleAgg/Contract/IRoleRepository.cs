using Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YasShop.Domain.Users.RoleAgg.Entities;
using YasShop.Domain.Users.UserAgg.Entities;

namespace YasShop.Domain.Users.RoleAgg.Contract
{
    public interface IRoleRepository : IRepository<tblRoles>
    {
        Task<IList<string>> GetRolesAsync(tblUsers user);
    }
}
