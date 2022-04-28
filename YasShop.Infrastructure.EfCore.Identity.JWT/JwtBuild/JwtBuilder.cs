using Framework.Application.Exceptions;
using Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Role;
using YasShop.Application.Contracts.ApplicationDTO.Users;
using YasShop.Application.Roles;
using YasShop.Application.Users;

namespace YasShop.Infrastructure.EfCore.Identity.JWT.JwtBuild
{
    public class JwtBuilder : IJwtBuilder
    {
        private readonly IUserApplication _UserApplication;
        private readonly IRoleApplication _RoleApplication;
        private readonly ILogger _Logger;
        public JwtBuilder(IUserApplication userApplication, IRoleApplication roleApplication, ILogger logger)
        {
            _UserApplication = userApplication;
            _RoleApplication = roleApplication;
            _Logger = logger;
        }

        public async Task<string> CreateTokenAsync(string UserId)
        {
            try
            {
                #region Validation

                #endregion Validation

                #region GetUser
                OutIGetAllDetailsForUser _User = null;
                {
                    _User = await _UserApplication.GetAllDetailsForUserAsync(new InpGetAllDetailsForUser()
                    {
                        UserId = UserId
                    });
                    if (_User == null)
                        throw new ArgumentInvalidException("UserId is invalid");
                }
                #endregion GetUser

                #region GetRole
                List<string> _Roles = null;
                {
                    _Roles = await _RoleApplication.GetRoleByUserAsync(new InpGetRoleByUser()
                    {
                        UserId = UserId
                    });
                    if (_Roles == null)
                        throw new ArgumentInvalidException("UserId is invalid");
                }
                #endregion GetRole


            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
