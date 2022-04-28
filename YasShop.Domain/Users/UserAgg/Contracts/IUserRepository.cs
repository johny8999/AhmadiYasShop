using Framework.Domain;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using YasShop.Domain.Users.UserAgg.Entities;

namespace YasShop.Domain.Users.UserAgg.Contracts
{
    public interface IUserRepository : IRepository<tblUsers>
    {
        Task<IdentityResult> AddAsync(tblUsers entity, string Password);
        Task<IdentityResult> AddToRolesAsync(tblUsers user, IEnumerable<string> roles);
        Task<IdentityResult> ConfirmEmailAsync(tblUsers user, string token);
        Task<tblUsers> FindByEmailAsync(string email);
        Task<tblUsers> FindByIdAsync(string userId);
        Task<string> GenerateEmailConfirmationTokenAsync(tblUsers user);
        Task<IList<string>> GetRolesAsync(tblUsers user);
        Task<SignInResult> PasswordSignInAsync(tblUsers user, string password, bool isPersistent, bool lockoutOnFailure);
        Task<IdentityResult> RemoveFromRolesAsync(tblUsers user, IEnumerable<string> roles);
    }
}
