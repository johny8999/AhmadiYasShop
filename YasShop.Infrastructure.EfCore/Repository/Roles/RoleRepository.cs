using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using YasShop.Domain.Users.RoleAgg.Contract;
using YasShop.Domain.Users.RoleAgg.Entities;
using YasShop.Domain.Users.UserAgg.Entities;
using YasShop.Infrastructure.EfCore.Context;

namespace YasShop.Infrastructure.EfCore.Repository.Roles
{
    public class RoleRepository : BaseRepository<tblRoles>, IRoleRepository
    {
        private readonly UserManager<tblUsers> _userManager;
        public RoleRepository(MainContext context, UserManager<tblUsers> userManager) : base(context)
        {
            _userManager = userManager;
        }
        public async Task<IList<string>> GetRolesAsync(tblUsers user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}
