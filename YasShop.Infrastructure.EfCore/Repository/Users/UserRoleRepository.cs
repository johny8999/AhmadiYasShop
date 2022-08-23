using Framework.Domain;
using YasShop.Domain.Users.UserAgg.Contracts;
using YasShop.Domain.Users.UserAgg.Entities;
using YasShop.Infrastructure.EfCore.Context;

namespace YasShop.Infrastructure.EfCore.Repository.Users;

public class UserRoleRepository : BaseRepository<tblUserRole>, IUserRoleRepository
{
    public UserRoleRepository(MainContext context) : base(context) { }
    
}
