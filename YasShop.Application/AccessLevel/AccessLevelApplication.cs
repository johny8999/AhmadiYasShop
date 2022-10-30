using Framework.Application.Exceptions;
using Framework.Application.Services.Localizer;
using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Infrastructure;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.AccessLevel;
using YasShop.Application.Contracts.ApplicationDTO.Result;
using YasShop.Application.Contracts.ApplicationDTO.Role;
using YasShop.Application.Contracts.ApplicationDTO.UserRole;
using YasShop.Application.Roles;
using YasShop.Application.UserRole;
using YasShop.Domain.Users.AccessLevelAgg.Contract;
using YasShop.Domain.Users.AccessLevelAgg.Entities;

namespace YasShop.Application.AccessLevel
{
    public class AccessLevelApplication : IAccessLevelApplication
    {
        private readonly IAccessLevelRepository _AccessLevelRepository;
        private readonly ILogger _Logger;
        private readonly ILocalizer _localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IAccessLevelRoleRepository _AccessLevelRoleRepository;
        private readonly IRoleApplication _RoleApplication;
        private readonly IUserRoleApplication _UserRoleApplication;
        public AccessLevelApplication(IAccessLevelRepository accessLevelRepository,
                                        ILogger logger, IServiceProvider serviceProvider,
                                         IAccessLevelRoleRepository accessLevelRoleRepository, ILocalizer localizer, IRoleApplication roleApplication, IUserRoleApplication userRoleApplication)
        {
            _AccessLevelRepository = accessLevelRepository;
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _AccessLevelRoleRepository = accessLevelRoleRepository;
            _localizer = localizer;
            _RoleApplication = roleApplication;
            _UserRoleApplication = userRoleApplication;
        }

        public async Task<string> GetIdByNameAsync(InpGetIdByName input)
        {
            try
            {
                input.CheckModelState(_ServiceProvider);
                return await _AccessLevelRepository.GetNoTraking.Where(a => a.Name == input.Name)
                    .Select(a => a.Id.ToString()).SingleOrDefaultAsync();

            }
            catch (ArgumentException ex)
            {
                _Logger.Debug(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<List<string>> GetUserRoleByAccessId(InpGetUserRoleByAccessId Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion Validation

                return await _AccessLevelRoleRepository.GetNoTraking.Where(a => a.AccessLevelId == Input.AccessLevelId.ToGuid())
                                .Select(a => a.RoleId.ToString()).ToListAsync();


            }
            catch (ArgumentException ex)
            {
                _Logger.Debug(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<string> GetAccessLevelIdbyNameAsync(InpGetAccessLevelIdbyName Input)
        {
            return await _AccessLevelRepository.GetNoTraking.Where(a => a.Name == Input.AccessLevelName).Select(a => a.Id.ToString()).FirstOrDefaultAsync();
        }

        public async Task<(OutPagingData PageData, List<OutGetAccessLevelForAdmin> LstItems)> GetAccessLevelForAdminAsync(InpGetAccessLevelForAdmin Input)
        {
            try
            {
                #region Validation
                {
                    Input.CheckModelState(_ServiceProvider);
                }
                #endregion Validation

                #region GetAccessLevel
                IQueryable<OutGetAccessLevelForAdmin> qData = null;
                {
                    qData = _AccessLevelRepository.GetNoTraking
                                    .Select(a => new OutGetAccessLevelForAdmin
                                    {
                                        Id = a.Id.ToString(),
                                        Name = a.Name,
                                        UserCount = a.tblUsers.Count()
                                    });

                }
                #endregion GetAccessLevel

                #region PagingDate
                OutPagingData _PagingData = null;
                {
                    int CountAllItem = await qData.CountAsync();
                    _PagingData = PagingData.Calculate(CountAllItem, Input.Page, Input.Take);
                }
                #endregion PagingDate
                return (_PagingData, await (qData.OrderBy(a => a.Name).Skip(_PagingData.Skip).Take(_PagingData.Take)).ToListAsync());
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return (null, null);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return (null, null);
            }
        }

        public async Task<OperationResult> DeleteAccessLevelAsync(InputDeleteAccessLevel input)
        {
            try
            {
                #region Validation
                {
                    input.CheckModelState(_ServiceProvider);
                }
                #endregion Validation

                #region GetAccessLevel

                var tAccessLevel = await _AccessLevelRepository.GetNoTraking
                     .Where(a => a.Id.ToString() == input.Id)
                     .Select(a => new
                     {
                         tAccLevel = a,
                         HasUser = a.tblUsers.Any()
                     })
                     .SingleOrDefaultAsync();

                if (tAccessLevel is null)
                    return new OperationResult().Failed(_localizer["Id not Found"]);

                if (tAccessLevel.HasUser)
                    return new OperationResult().Failed(_localizer["AccessLevel has User"]);
                #endregion GetAccessLevel

                #region DeleteAccessLevel
                {
                    await _AccessLevelRepository.DeleteAsync(tAccessLevel.tAccLevel);
                }
                #endregion DeleteAccessLevel

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);

            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed(_localizer["Error500"]);
            }
        }

        public async Task<OperationResult> AddAccessLevelAsync(InpAddAccessLevel Input)
        {
            try
            {
                #region Validation
                {
                    Input.CheckModelState(_ServiceProvider);
                    if (_AccessLevelRepository.GetNoTraking.Any(a => a.Name == Input.Name))
                        return new OperationResult().Failed("AccessLevel was existed");
                }
                #endregion Validation

                #region Add AccessLeve by name
                Guid _AccessLevelId = new Guid().SequentialGuid();
                {
                    await _AccessLevelRepository.AddAsync(new tblAccessLevel
                    {
                        Id = _AccessLevelId,
                        Name = Input.Name,
                    });
                }
                #endregion Add AccessLeve by name

                #region Add Access level roles
                {
                    await _AccessLevelRoleRepository.AddRangeAsync(Input.Roles.Select(_RoleName => new tblAccessLevelRoles
                    {
                        Id = new Guid().SequentialGuid(),
                        AccessLevelId = _AccessLevelId,
                        RoleId = (_RoleApplication.GetIdByRoleNameAsync(new InpGetIdByRoleName { RoleName = _RoleName })).Result.Data.ToGuid()
                    }));
                }
                #endregion Add Access level roles

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult<OutGetAccessLevelForEdit>> GetAccessLevelForEditAsync(InpGetAccessLevelForEdit Input)
        {
            try
            {
                #region Validation
                {
                    Input.CheckModelState(_ServiceProvider);
                }
                #endregion Validation

                //var _tAccLevel =await _AccessLevelRepository.GetById(Input.Id);
                var _tAccLevel = await _AccessLevelRepository.GetNoTraking
                    .Where(a => a.Id == Input.Id.ToGuid())
                    //.Select(a=>a.Name)
                    .ProjectToType<OutGetAccessLevelForEdit>()
                    .SingleOrDefaultAsync();

                if (_tAccLevel is null)
                    return new OperationResult<OutGetAccessLevelForEdit>().Failed("AccessLevel is invlid");

                return new OperationResult<OutGetAccessLevelForEdit>().Succeeded(_tAccLevel.Adapt<OutGetAccessLevelForEdit>());
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex.Message);
                return new OperationResult<OutGetAccessLevelForEdit>().Failed("Error500");
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message);
                return new OperationResult<OutGetAccessLevelForEdit>().Failed("Error500");
            }
        }

        public async Task<OperationResult> EditAccessLevelAsync(InpEditAccessLevel Input)
        {
            try
            {
                #region Validation
                {
                    Input.CheckModelState(_ServiceProvider);
                }
                #endregion Validation

                #region UpdateAccessLevel
                {
                    var _tAccessLevel = await _AccessLevelRepository.GetById(Input.Id);

                    _tAccessLevel.Name = Input.Name;

                    await _AccessLevelRepository.UpdateAsync(_tAccessLevel);
                }
                #endregion UpdateAccessLevel

                #region UpdateAccessLevelRoles
                {
                    #region Delete Old AccessLevel Roles
                    {
                        var qAccessLevelRoles = await _AccessLevelRoleRepository.GetNoTraking
                                                .Where(a => a.AccessLevelId == Input.Id.ToGuid())
                                                .ToListAsync();
                        await _AccessLevelRoleRepository.DeleteRangeAsync(qAccessLevelRoles);
                    }
                    #endregion Delete Old AccessLevel Roles

                    #region Add new AccessLevel Roles
                    {
                        await _AccessLevelRoleRepository.AddRangeAsync(Input.Roles.Select(a => new tblAccessLevelRoles
                        {
                            Id = new Guid().SequentialGuid(),
                            AccessLevelId = Input.Id.ToGuid(),
                            RoleId = _RoleApplication.GetIdByRoleNameAsync(new InpGetIdByRoleName
                            {
                                RoleName = a
                            }).Result.Data.ToGuid()
                        }));
                    }
                    #endregion Add new AccessLevel Roles
                }
                #endregion UpdateAccessLevelRoles

                #region UpdateUserRole
                {
                    var qAccessLevelRole = await _AccessLevelRoleRepository.GetNoTraking
                                                    .Where(a => a.AccessLevelId == Input.Id.ToGuid())
                                                    .Select(a => a.RoleId.ToString())
                                                    .ToListAsync();

                    var Result = await GetUserIdsByAccessLevelIds(new InpGetUserIdsByAccessLevelId { AccessLevelIds = Input.Id });

                    if (!Result.IsSuccess)
                        return new OperationResult().Failed(Result.Message);

                    foreach (var item in Result.Data) //Data:UserId
                    {
                        var ResultUpdateUserRoles = await _UserRoleApplication.UpdateUserRoles(new InpUpdateUserRolesDto
                        {
                            UserId = item,
                           // RolesId = qAccessLevelRole.Select(a => a.ToString()).ToArray()
                           RolesId=qAccessLevelRole.ToArray()
                        });
                    }
                }
                #endregion UpdateUserRole

                return new OperationResult().Succeeded();

            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message);
                return new OperationResult().Failed("Error500");

            }
        }

        public async Task<OperationResult<List<string>>> GetUserIdsByAccessLevelIds(InpGetUserIdsByAccessLevelId Input)
        {
            try
            {
                #region Validation
                {
                    Input.CheckModelState(_ServiceProvider);
                }
                #endregion Validation

                var Result = await _AccessLevelRepository.GetNoTraking
                                        .Where(a => a.Id == Input.AccessLevelIds.ToGuid())
                                        .SelectMany(a => a.tblUsers.Select(b => b.Id.ToString()))
                                        .ToListAsync();
                return new OperationResult<List<string>>().Succeeded(Result);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<List<string>>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<List<string>>().Failed(ex.Message);
            }
        }

    }
}
