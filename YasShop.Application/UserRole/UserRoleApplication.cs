using Framework.Application.Exceptions;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Result;
using YasShop.Application.Contracts.ApplicationDTO.UserRole;
using YasShop.Domain.Users.UserAgg.Contracts;
using YasShop.Domain.Users.UserAgg.Entities;

namespace YasShop.Application.UserRole
{
    public sealed class UserRoleApplication: IUserRoleApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IUserRoleRepository _UserRoleRepository;
        public UserRoleApplication(ILogger logger, IServiceProvider serviceProvider, IUserRoleRepository userRoleRepository)
        {
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _UserRoleRepository = userRoleRepository;
        }

        public async Task<OperationResult> UpdateUserRoles(InpUpdateUserRolesDto Input)
        {
            try
            {
                #region Validation
                {
                    Input.CheckModelState(_ServiceProvider);
                }
                #endregion Validation

                var qDeleteUserRole = await _UserRoleRepository.GetNoTraking.Where(a => a.Id == Input.Id.ToGuid()).SingleOrDefault();
            }
            catch(ArgumentInvalidException ex)
            {
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }
    }
}
