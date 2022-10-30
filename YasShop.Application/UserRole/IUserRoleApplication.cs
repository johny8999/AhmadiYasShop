using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Result;
using YasShop.Application.Contracts.ApplicationDTO.UserRole;

namespace YasShop.Application.UserRole
{
    public interface IUserRoleApplication
    {
        Task<OperationResult> UpdateUserRoles(InpUpdateUserRolesDto Input);
    }
}