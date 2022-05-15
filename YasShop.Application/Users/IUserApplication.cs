using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Result;
using YasShop.Application.Contracts.ApplicationDTO.Users;
using YasShop.Domain.Users.UserAgg.Entities;

namespace YasShop.Application.Users
{
    public interface IUserApplication
    {
        Task<OperationResult> ChangeUserAccessLevelAsync(InpChangeUserAccessLevel Input);
        Task<OperationResult> ChangeUserRoleByAccessLevelIdAsync(InpChangeUserRoleByAccessLevelId Input);
        Task<OperationResult> EmailConfirmationAsync(InpEmailConfirmation Input);
        Task<tblUsers> FindByIdAsync(string userId);
        Task<OperationResult> ForgetPasswordAsync(InpForgetPassword Input);
        Task<OutIGetAllDetailsForUser> GetAllDetailsForUserAsync(InpGetAllDetailsForUser input);
        Task<OperationResult> LoginByEmailPasswordAsync(InpLoginByEmailPassword input);
        Task<OperationResult> RegisterByEmailPasswordAsync(InpRegisterByEmailPassword Input);
    }
}