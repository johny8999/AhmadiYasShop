using System.Collections.Generic;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Result;
using YasShop.Application.Contracts.ApplicationDTO.Role;

namespace YasShop.Application.Roles
{
    public interface IRoleApplication
    {
        Task<List<string>> GetRoleByUserAsync(InpGetRoleByUser input);
        Task<OperationResult<List<string>>> GetRoleNamesByAccessLevelIdAsync(InpGetRoleNamesByAccessLevelId Input);
    }
}