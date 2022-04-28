using System.Collections.Generic;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Role;

namespace YasShop.Application.Roles
{
    public interface IRoleApplication
    {
        Task<List<string>> GetRoleByUserAsync(InpGetRoleByUser input);
    }
}