using Framework.Common.Utilities.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.AccessLevel;
using YasShop.Application.Contracts.ApplicationDTO.Result;

namespace YasShop.Application.AccessLevel
{
    public interface IAccessLevelApplication
    {
        Task<OperationResult> AddAccessLevelAsync(InpAddAccessLevel Input);
        Task<bool> CheckExistAccLevelAsync(InpCheckExistAccLevel input);
        Task<OperationResult> DeleteAccessLevelAsync(InputDeleteAccessLevel input);
        Task<OperationResult> EditAccessLevelAsync(InpEditAccessLevel Input);
        Task<(OutPagingData PageData, List<OutGetAccessLevelForAdmin> LstItems)> GetAccessLevelForAdminAsync(InpGetAccessLevelForAdmin Input);
        Task<OperationResult<OutGetAccessLevelForEdit>> GetAccessLevelForEditAsync(InpGetAccessLevelForEdit Input);
        Task<string> GetAccessLevelIdbyNameAsync(InpGetAccessLevelIdbyName Input);
        Task<string> GetIdByNameAsync(InpGetIdByName input);
        Task<OperationResult<List<string>>> GetUserIdsByAccessLevelIds(InpGetUserIdsByAccessLevelId Input);
        Task<List<string>> GetUserRoleByAccessId(InpGetUserRoleByAccessId Input);
    }
}