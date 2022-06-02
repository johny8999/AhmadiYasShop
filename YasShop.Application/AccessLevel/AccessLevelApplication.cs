using Framework.Application.Exceptions;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.AccessLevel;
using YasShop.Domain.Users.AccessLevelAgg.Contract;

namespace YasShop.Application.AccessLevel
{
    public class AccessLevelApplication : IAccessLevelApplication
    {
        private readonly IAccessLevelRepository _AccessLevelRepository;
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IAccessLevelRoleRepository _AccessLevelRoleRepository;
        public AccessLevelApplication(IAccessLevelRepository accessLevelRepository,
                                        ILogger logger, IServiceProvider serviceProvider,
                                         IAccessLevelRoleRepository accessLevelRoleRepository)
        {
            _AccessLevelRepository = accessLevelRepository;
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _AccessLevelRoleRepository = accessLevelRoleRepository;
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

        public async Task<OutGetAccessLevelForAdmin> GetAccessLevelForAdmin(InpGetAccessLevelForAdmin Input)
        {
            try
            {
                #region Validation
                {
                    Input.CheckModelState(_ServiceProvider);
                }
                #endregion Validation

                #region GetAccessLevel
                IQueryable<OutGetAccessLevelForAdmin> qGet = null;
                {
                    qGet = _AccessLevelRepository.GetNoTraking
                                    .Select(a => new OutGetAccessLevelForAdmin
                                    {
                                        Id = a.Id.ToString(),
                                        Name = a.Name,
                                        UserCount = a.tblUsers.Count()
                                    });

                }
                #endregion GetAccessLevel

                #region PagingDate
                {
                    qGet.Skip(Input.Page).Take(Input.Take);
                }
                #endregion PagingDate
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
