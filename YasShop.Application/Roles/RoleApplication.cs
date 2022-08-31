using Framework.Application.Exceptions;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Result;
using YasShop.Application.Contracts.ApplicationDTO.Role;
using YasShop.Application.Users;
using YasShop.Domain.Users.AccessLevelAgg.Contract;
using YasShop.Domain.Users.RoleAgg.Contract;

namespace YasShop.Application.Roles;

public class RoleApplication : IRoleApplication
{
    private readonly ILogger _Logger;
    private readonly IRoleRepository _RoleRepository;
    private readonly IServiceProvider _ServiceProvider;
    private readonly IUserApplication _UserApplication;
    private readonly IAccessLevelRoleRepository _AccessLevelRoleRepository;
    public RoleApplication(ILogger logger, IRoleRepository roleRepository, IServiceProvider serviceProvider, IUserApplication userApplication, IAccessLevelRoleRepository accessLevelRoleRepository)
    {
        _Logger = logger;
        _RoleRepository = roleRepository;
        _ServiceProvider = serviceProvider;
        _UserApplication = userApplication;
        _AccessLevelRoleRepository = accessLevelRoleRepository;
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
            #region Validation
            {
                Input.CheckModelState(_ServiceProvider);
            }
            #endregion Validation

            #region Get Roles Name
            List<string> qRoleNames = null;
            {
                qRoleNames = await _AccessLevelRoleRepository.GetNoTraking
                                                   .Where(a => a.AccessLevelId.Equals(Input.AccessLevelId.ToGuid()))
                                                   .Select(a => a.tblRole.Name)
                                                   .ToListAsync();

                //var q =  _RoleRepository.GetNoTraking
                //                        .Where(b => b.tblAccessLevelRoles.Any(c=>c.AccessLevelId.Equals(Input.AccessLevelId.ToGuid())))
                //                        .Select(a => a.Name);
            }
            #endregion Get Roles Name

            return new OperationResult<List<string>>().Succeeded(qRoleNames);
        }
        catch (ArgumentInvalidException ex)
        {
            _Logger.Debug(ex);
            return new OperationResult<List<string>>().Failed(ex.Message);
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
            return new OperationResult<List<string>>().Failed("Error500");
        }
    }

    public async Task<OperationResult<List<OutGetAllRolesByParentId>>> GetAllRolesByParentIdAsync(InpGetAllRolesByParentId Input)
    {
        try
        {

            #region Validation
            {
                Input.CheckModelState(_ServiceProvider);
            }
            #endregion Validation

            #region Get Roles By ParentId
            List<OutGetAllRolesByParentId> qData = null;
            {
                qData = await _RoleRepository.GetNoTraking
                                        .Where(a => a.ParentId == (Input.ParentId == null ? null : Input.ParentId.ToGuid()))
                                         .Select(a => new OutGetAllRolesByParentId
                                         {
                                             Id = a.Id.ToString(),
                                             RoleName = a.Name,
                                             PageName = a.PageName,
                                             ParentId = a.ParentId.ToString(),
                                             HasChild = a.tblRolesChilds.Any(),
                                             Description = a.Description
                                         })
                                         .ToListAsync();

                //qData = await (from a in _RoleRepository.GetNoTraking
                //               where a.ParentId != null ? a.ParentId == Input.ParentId.ToGuid() : true
                //               let hasChild=a.tblRolesChilds.Any()
                //               select new OutGetAllRolesByParentId
                //               {
                //                   Id = a.Id.ToString(),
                //                   RoleName = a.Name,
                //                   PageName = a.PageName,
                //                   ParentId = a.ParentId.ToString(),
                //                   HasChild = hasChild,
                //                   Description = a.Description
                //               }).ToListAsync();
            }
            #endregion Get Roles By ParentId
            return new OperationResult<List<OutGetAllRolesByParentId>>().Succeeded(qData);
        }
        catch (ArgumentInvalidException ex)
        {
            _Logger.Debug(ex);
            return new OperationResult<List<OutGetAllRolesByParentId>>().Failed(ex.Message);
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
            return new OperationResult<List<OutGetAllRolesByParentId>>().Failed("Error500");
        }
    }

    public async Task<OperationResult<string>> GetIdByRoleNameAsync(InpGetIdByRoleName Input)
    {
        try
        {
            #region Validation
            {
                Input.CheckModelState(_ServiceProvider);
            }
            #endregion Validation

            var _Id = await _RoleRepository.GetNoTraking.Where(a => a.Name == Input.RoleName).Select(a => a.Id.ToString()).SingleOrDefaultAsync();
            return new OperationResult<string>().Succeeded(_Id);
        }
        catch (ArgumentInvalidException ex)
        {
            _Logger.Debug(ex);
            return new OperationResult<string>().Failed(ex.Message);
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
            return new OperationResult<string>().Failed("Error500");
        }
    }
}
