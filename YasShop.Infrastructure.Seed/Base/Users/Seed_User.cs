using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using YasShop.Application.AccessLevel;
using YasShop.Application.Contracts.ApplicationDTO.AccessLevel;
using YasShop.Domain.Users.RoleAgg.Contract;
using YasShop.Domain.Users.UserAgg.Contracts;
using YasShop.Domain.Users.UserAgg.Entities;

namespace YasShop.Infrastructure.Seed.Base.Users
{
    public class Seed_User : ISeed_User
    {
        private readonly IUserRepository _UserRepository;
        private readonly IUserRoleRepository _UserRoleRepository;
        private readonly IRoleRepository _RoleRepository;
        private readonly ILogger _Logger;
        private readonly IAccessLevelApplication _AccessLevelApplication;
        public Seed_User(IUserRepository userRepository, ILogger logger, IAccessLevelApplication accessLevelApplication, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            _UserRepository = userRepository;
            _Logger = logger;
            _AccessLevelApplication = accessLevelApplication;
            _UserRoleRepository = userRoleRepository;
            _RoleRepository = roleRepository;
        }

        public async Task<bool> RunAsync()
        {
            try
            {
                #region Add Sina User
                {
                    var _Id = new Guid().SequentialGuid();
                    string UserEmail = "sinaalipour89@gmail.com";
                    if (!await _UserRepository.GetNoTraking.AnyAsync(a => a.Email == UserEmail))
                    {
                        #region Seed User
                        {

                            await _UserRepository.AddAsync(new tblUsers()
                            {
                                Id = _Id,
                                Email = "sinaalipour89@gmail.com",
                                NormalizedEmail = "sinaalipour89@gmail.com".ToUpper(),
                                FullName = "سینا عالیپور",
                                Date = DateTime.Now,
                                IsActive = true,
                                UserName = "sinaalipour89@gmail.com",
                                NormalizedUserName = "sinaalipour8999".ToUpper(),
                                EmailConfirmed = true,
                                PasswordHash = "AQAAAAEAACcQAAAAEGXT98P0X0495qWpYE1/vEpMA1ZgkDxQ77iUENoFOBS93TEDMwcOhFNVUlAMQcTfEw==",
                                SecurityStamp = "DVF26VWYYHBBJSI5F3BFKQWPUCWVYLO6",
                                ConcurrencyStamp = "001061fb-c46b-43d4-9198-5e5557188802",
                                AccessLevelId = (await _AccessLevelApplication.GetIdByNameAsync
                                    (new InpGetIdByName() { Name = "NotConfirmedUser" })).ToGuid(),
                            });
                        }
                        #endregion Seed User

                    }
                    else
                    {
                        _Id = await _UserRepository.GetNoTraking.Where(a => a.Email == UserEmail).Select(a => a.Id).SingleAsync();
                    }
                    #region Delete User Role
                    {
                        var AllRoleUser = await _UserRoleRepository.GetNoTraking.Where(a => a.UserId == _Id).ToListAsync();
                        await _UserRoleRepository.DeleteRangeAsync(AllRoleUser);
                    }
                    #endregion Delete User Role

                    #region Add User Roles
                    {
                        var AllRoles = await _RoleRepository.GetNoTraking.Select(a => a.Id).ToListAsync();
                        await _UserRoleRepository.AddRangeAsync(AllRoles.Select(a => new tblUserRole
                        {
                           Id=new Guid().SequentialGuid(),
                           UserId=_Id,
                           RoleId=a
                        }));
                    }
                    #endregion Add User Roles
                }
                #endregion Add Sina User

                return true;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }
    }
}
