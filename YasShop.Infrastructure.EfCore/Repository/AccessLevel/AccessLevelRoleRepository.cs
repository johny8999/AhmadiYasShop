using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YasShop.Domain.Users.AccessLevelAgg.Contract;
using YasShop.Domain.Users.AccessLevelAgg.Entities;
using YasShop.Infrastructure.EfCore.Context;

namespace YasShop.Infrastructure.EfCore.Repository.AccessLevel
{
    public class AccessLevelRoleRepository : BaseRepository<tblAccessLevelRoles>, IAccessLevelRoleRepository
    {
        public AccessLevelRoleRepository(MainContext context) : base(context)
        {
        }
    }
}
