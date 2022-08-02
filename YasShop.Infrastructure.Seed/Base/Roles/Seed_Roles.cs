using Framework.Application.Exceptions;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
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
                if (await _RoleRepository.GetNoTraking.AnyAsync(a => a.Name == "CanManageAccessLevel"))
                    await _RoleRepository.AddAsync(new tblRoles
                    {
                        Id=_Id,
                        ParentId=null,
                        PageName= "AccessLevelPage",
                        Name = "CanManageAccessLevel",
                        NormalizedName= "CanManageAccessLevel".ToUpper(),
                        Sort=10,
                        Description="توانایی مدیریت سطوح دسترسی"
                    },false);
            }
            #endregion Manage AccessLevel Page
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
