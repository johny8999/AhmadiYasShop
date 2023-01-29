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
