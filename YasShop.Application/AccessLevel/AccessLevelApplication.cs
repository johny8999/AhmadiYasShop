using Framework.Application.Exceptions;
using Framework.Application.Services.Localizer;
using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.AccessLevel;
using YasShop.Application.Contracts.ApplicationDTO.Result;
using YasShop.Domain.Users.AccessLevelAgg.Contract;

namespace YasShop.Application.AccessLevel
{
    public class AccessLevelApplication : IAccessLevelApplication
    {
        private readonly IAccessLevelRepository _AccessLevelRepository;
        private readonly ILogger _Logger;
        private readonly ILocalizer _localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IAccessLevelRoleRepository _AccessLevelRoleRepository;
        public AccessLevelApplication(IAccessLevelRepository accessLevelRepository,
                                        ILogger logger, IServiceProvider serviceProvider,
                                         IAccessLevelRoleRepository accessLevelRoleRepository, ILocalizer localizer)
        {
            _AccessLevelRepository = accessLevelRepository;
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _AccessLevelRoleRepository = accessLevelRoleRepository;
            _localizer = localizer;
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

                if(tAccessLevel.HasUser)
                    return new OperationResult().Failed(_localizer["AccessLevel has User"]);
                #endregion GetAccessLevel

                #region DeleteAccessLevel
                {
                    await _AccessLevelRepository.DeleteAsync(tAccessLevel.tAccLevel);
                }
                #endregion DeleteAccessLevel

                return new OperationResult().Successed();
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
    }
}
