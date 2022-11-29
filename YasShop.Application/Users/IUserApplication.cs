using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Result;
using YasShop.Application.Contracts.ApplicationDTO.Users;
using YasShop.Application.Contracts.PresentationDTO.input;
using YasShop.Domain.Users.UserAgg.Entities;

namespace YasShop.Application.Users
{
    public interface IUserApplication
    {
        Task<OperationResult> ChangeUserAccessLevelAsync(InpChangeUserAccessLevel Input);
        Task<OperationResult> ChangeUserAccLevelAsync(InpChangeUserAccLevel Input);
        Task<OperationResult> ChangeUserRoleByAccessLevelIdAsync(InpChangeUserRoleByAccessLevelId Input);
        Task<OperationResult> EmailConfirmationAsync(InpEmailConfirmation Input);
        Task<tblUsers> FindByEmailAsync(string Email);
        Task<tblUsers> FindByIdAsync(string userId);
        Task<OperationResult> ForgetPasswordAsync(InpForgetPassword Input);
        Task<OutIGetAllDetailsForUser> GetAllDetailsForUserAsync(InpGetAllDetailsForUser input);
        Task<OperationResult> LoginByEmailPasswordAsync(InpLoginByEmailPassword input);
        Task<OperationResult> LoginByPhoneNumberStep1Async(InpLoginByPhoneNumberStep1 Input);
        Task<OperationResult<string>> LoginByPhoneNumberStep2Async(InpLoginByPhoneNumberStep2 Input);
        Task<OperationResult> RegisterByEmailPasswordAsync(InpRegisterByEmailPassword Input);
        Task<OperationResult> ResendOtpCodeAsync(InpResendOtpCode Input);
        Task<OperationResult> ResetPasswordAsync(InpResetPassword Input);
    }
}