using Framework.Common.Utilities.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.AccessLevel;

namespace YasShop.Application.AccessLevel
{
    public interface IAccessLevelApplication
    {
        Task<(OutPagingData PageData, List<OutGetAccessLevelForAdmin> LstItems)> GetAccessLevelForAdminAsync(InpGetAccessLevelForAdmin Input);
        Task<string> GetAccessLevelIdbyNameAsync(InpGetAccessLevelIdbyName Input);
        Task<string> GetIdByNameAsync(InpGetIdByName input);
        Task<List<string>> GetUserRoleByAccessId(InpGetUserRoleByAccessId Input);
    }
}