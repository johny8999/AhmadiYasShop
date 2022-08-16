using Framework.Application.Exceptions;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using YasShop.Domain.Users.RoleAgg.Contract;
using YasShop.Domain.Users.RoleAgg.Entities;

namespace YasShop.Infrastructure.Seed.Base.Roles;

public class Seed_Roles : ISeed_Roles
{
    private readonly ILogger _Logger;
    private readonly IRoleRepository _RoleRepository;

    public Seed_Roles(ILogger logger, IRoleRepository roleRepository)
    {
        _Logger = logger;
        _RoleRepository = roleRepository;
    }

    public async Task<bool> RunAsync()
    {
        try
        {
            #region AdminPage
            {
                if (!await _RoleRepository.GetNoTraking.AnyAsync(a => a.Name == "AdminPage"))
                    await _RoleRepository.AddAsync(new tblRoles
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = null,
                        PageName = "AdminPage",
                        Name = "AdminPage",
                        NormalizedName = "AdminPage".ToUpper(),
                        Sort = 0,
                        Description = "توانایی دسترسی به پنل مدیریت",
                    }, false);
            }
            #endregion AdminPage

            #region Manage AccessLevel Page
            {
                Guid _Id = new Guid().SequentialGuid();
                if (!await _RoleRepository.GetNoTraking.AnyAsync(a => a.Name == "CanManageAccessLevel"))
                {
                    await _RoleRepository.AddAsync(new tblRoles
                    {
                        Id = _Id,
                        ParentId = null,
                        PageName = "AccessLevelPage",
                        Name = "CanManageAccessLevel",
                        NormalizedName = "CanManageAccessLevel".ToUpper(),
                        Sort = 10,
                        Description = "توانایی مدیریت سطوح دسترسی"
                    }, false);
                }
                else
                {
                    _Id = await _RoleRepository.GetNoTraking.Where(a => a.Name == "CanManageAccessLevel").Select(a => a.Id).SingleOrDefaultAsync();
                }

                if (!await _RoleRepository.GetNoTraking.AnyAsync(a => a.Name == "CanViewListAccessLevel"))
                {
                    await _RoleRepository.AddAsync(new tblRoles
                    {
                        Id = new Guid().SequentialGuid(),
                        Name = "CanViewListAccessLevel",
                        NormalizedName = "CanViewListAccessLevel".ToUpper(),
                        PageName = "AccessLevelPage",
                        ParentId = _Id,
                        Sort = 20,
                        Description = "توانایی مشاهده سطح دسترسی"
                    });
                }

                //Add
                if (!await _RoleRepository.GetNoTraking.AnyAsync(a => a.Name == "CanAddAccessLevel"))
                {
                    await _RoleRepository.AddAsync(new tblRoles
                    {
                        Id = _Id,
                        ParentId = null,
                        PageName = "AccessLevelPage",
                        Name = "CanAddAccessLevel",
                        NormalizedName = "CanAddAccessLevel".ToUpper(),
                        Sort = 20,
                        Description = "توانایی افزودن سطح دسترسی"
                    }, false);
                }

                //Edit
                if (!await _RoleRepository.GetNoTraking.AnyAsync(a => a.Name == "CanEditAccessLevel"))
                {
                    await _RoleRepository.AddAsync(new tblRoles
                    {
                        Id = _Id,
                        ParentId = null,
                        PageName = "AccessLevelPage",
                        Name = "CanEditAccessLevel",
                        NormalizedName = "CanEditAccessLevel".ToUpper(),
                        Sort = 30,
                        Description = "توانایی ویرایش سطح دسترسی"
                    }, false);
                }

                //Delete
                if (!await _RoleRepository.GetNoTraking.AnyAsync(a => a.Name == "CanDeleteAccessLevel"))
                {
                    await _RoleRepository.AddAsync(new tblRoles
                    {
                        Id = new Guid().SequentialGuid(),
                        Name = "CanDeleteAccessLevel",
                        NormalizedName = "CanDeleteAccessLevel".ToUpper(),
                        PageName = "AccessLevelPage",
                        ParentId = _Id,
                        Sort = 40,
                        Description = "توانایی حذف سطح دسترسی"
                    });
                }


            }

            #endregion Manage AccessLevel Page

            #region Manage User Page
            {
                Guid _Id = new Guid().SequentialGuid();

                if (!await _RoleRepository.GetNoTraking.AnyAsync(a => a.Name == "CanManageUser"))
                {
                    await _RoleRepository.AddAsync(new tblRoles
                    {
                        Id = _Id,
                        ParentId = null,
                        PageName = "UserPage",
                        Name = "CanManageUser",
                        NormalizedName = "CanManageUser".ToUpper(),
                        Sort = 10,
                        Description = "توانایی مدیریت کاربران"
                    }, false);
                }
                else
                {
                    _Id = await _RoleRepository.GetNoTraking.Where(a => a.Name == "CanManageAccessLevel").Select(a => a.Id).SingleOrDefaultAsync();
                }
                //EditUser
                if (!await _RoleRepository.GetNoTraking.AnyAsync(a => a.Name == "CanEditUser"))
                {
                    await _RoleRepository.AddAsync(new tblRoles
                    {
                        Id = new Guid().SequentialGuid(),
                        Name = "CanEditUser",
                        NormalizedName = "CanEditUser".ToUpper(),
                        PageName = "CanEditUser",
                        ParentId = _Id,
                        Sort = 50,
                        Description = "توانایی ویرایش کاربر"
                    });
                }

                //DeleteUser
                if (!await _RoleRepository.GetNoTraking.AnyAsync(a => a.Name == "CanDeleteUser"))
                {
                    await _RoleRepository.AddAsync(new tblRoles
                    {
                        Id = new Guid().SequentialGuid(),
                        Name = "CanDeleteUser",
                        NormalizedName = "CanDeleteUser".ToUpper(),
                        PageName = "AccessLevelPage",
                        ParentId = _Id,
                        Sort = 60,
                        Description = "توانایی حذف کاربر"
                    });
                }

                //LockUser
                if (!await _RoleRepository.GetNoTraking.AnyAsync(a => a.Name == "CanLockUser"))
                {
                    await _RoleRepository.AddAsync(new tblRoles
                    {
                        Id = new Guid().SequentialGuid(),
                        Name = "CanLockUser",
                        NormalizedName = "CanLockUser".ToUpper(),
                        PageName = "AccessLevelPage",
                        ParentId = _Id,
                        Sort = 60,
                        Description = "توانایی قفل حساب کاربر"
                    });
                }

                //UnlockUser
                if (!await _RoleRepository.GetNoTraking.AnyAsync(a => a.Name == "CanUnlockUser"))
                {
                    await _RoleRepository.AddAsync(new tblRoles
                    {
                        Id = new Guid().SequentialGuid(),
                        Name = "CanUnlockUser",
                        NormalizedName = "CanUnlockUser".ToUpper(),
                        PageName = "AccessLevelPage",
                        ParentId = _Id,
                        Sort = 60,
                        Description = "توانایی باز کردن قفل حساب کاربر"
                    });
                }
            }
            #endregion
        }
        catch (ArgumentInvalidException ex)
        {
            _Logger.Debug(ex);
            return default;//false
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
            return default;
        }
    }
}
