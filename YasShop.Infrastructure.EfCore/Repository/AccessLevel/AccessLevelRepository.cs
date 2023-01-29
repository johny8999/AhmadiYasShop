using YasShop.Domain.Users.AccessLevelAgg.Contract;
using YasShop.Domain.Users.AccessLevelAgg.Entities;
using YasShop.Infrastructure.EfCore.Context;

namespace YasShop.Infrastructure.EfCore.Repository.AccessLevel
{
    public class AccessLevelRepository : BaseRepository<tblAccessLevel>, IAccessLevelRepository
    {
        public AccessLevelRepository(MainContext context) : base(context)
        {
        }
    }
}
