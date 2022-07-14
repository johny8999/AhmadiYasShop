using Framework.Application.Exceptions;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Result;
using YasShop.Application.Contracts.ApplicationDTO.Role;
using YasShop.Application.Users;
using YasShop.Domain.Users.RoleAgg.Contract;

namespace YasShop.Application.Roles;

public class RoleApplication : IRoleApplication
{
    private readonly ILogger _Logger;
    private readonly IRoleRepository _RoleRepository;
    private readonly IServiceProvider _ServiceProvider;
    private readonly IUserApplication _UserApplication;

    public RoleApplication(ILogger logger, IRoleRepository roleRepository, IServiceProvider serviceProvider, IUserApplication userApplication)
    {
        _Logger = logger;
        _RoleRepository = roleRepository;
        _ServiceProvider = serviceProvider;
        _UserApplication = userApplication;
    }

    public async Task<List<string>> GetRoleByUserAsync(InpGetRoleByUser input)
    {
        try
        {
            #region Validation
            input.CheckModelState(_ServiceProvider);
            #endregion Validation

            var qUser = await _UserApplication.FindByIdAsync(input.UserId);
            return (await _RoleRepository.GetRolesAsync(qUser)).ToList();
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

    public async Task<OperationResult<List<string>>> GetRoleNamesByAccessLevelIdAsync(InpGetRoleNamesByAccessLevelId Input)
    {
        try
        {

        }
        catch (ArgumentInvalidException ex)
        {
            _Logger.Debug(ex.Message);
            return new OperationResult<>
        }
        catch (Exception)
        {

            throw;
        }
    }

}
