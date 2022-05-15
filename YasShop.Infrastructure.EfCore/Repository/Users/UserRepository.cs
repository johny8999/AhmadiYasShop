using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using YasShop.Domain.Users.UserAgg.Contracts;
using YasShop.Domain.Users.UserAgg.Entities;
using YasShop.Infrastructure.EfCore.Context;

namespace YasShop.Infrastructure.EfCore.Repository.Users
{
    public class UserRepository : BaseRepository<tblUsers>, IUserRepository
    {
        private readonly UserManager<tblUsers> _UserManager;
        private readonly SignInManager<tblUsers> _SignInManager;
        public UserRepository(MainContext context, UserManager<tblUsers> userManager
            , SignInManager<tblUsers> signInManager) : base(context)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
        }

        public async Task<IdentityResult> AddAsync(tblUsers entity, string Password)
        {
            return await _UserManager.CreateAsync(entity, Password);
        }

        public async Task<tblUsers> FindByIdAsync(string userId)
        {
            return await _UserManager.FindByIdAsync(userId);
        }

        public async Task<tblUsers> FindByEmailAsync(string email)
        {
            return await _UserManager.FindByEmailAsync(email);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(tblUsers user)
        {
            return await _UserManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(tblUsers user, string token)
        {
            return await _UserManager.ConfirmEmailAsync(user, token);
        }
        
        public async Task<IList<string>> GetRolesAsync(tblUsers user)
        {
            return await _UserManager.GetRolesAsync(user);
        }
        public async Task<IdentityResult> RemoveFromRolesAsync(tblUsers user, IEnumerable<string> roles)
        {
            return await _UserManager.RemoveFromRolesAsync(user, roles);
        }

        public async Task<IdentityResult> AddToRolesAsync(tblUsers user, IEnumerable<string> roles)
        {
            return await _UserManager.AddToRolesAsync(user, roles);
        }

        public async Task<SignInResult> PasswordSignInAsync(tblUsers user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await _SignInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(tblUsers user)
        {
            return await _UserManager.GeneratePasswordResetTokenAsync(user);
        }
    }
}
