using Framework.Application.Exceptions;
using Framework.Infrastructure;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Role;
using YasShop.Application.Contracts.PresentationDTO.ViewInput;
using YasShop.Application.Roles;

namespace YasShop.WebApp.Pages.Admin.AccessLevels.Components;

[Authorize]
public class Compo_ListRolesModel : PageModel
{
    private readonly ILogger _Logger;
    private readonly IServiceProvider _ServiceProvider;
    private readonly IRoleApplication _RoleApplication;

    public Compo_ListRolesModel(IRoleApplication roleApplication, ILogger logger, IServiceProvider serviceProvider)
    {
        _RoleApplication = roleApplication;
        _Logger = logger;
        _ServiceProvider = serviceProvider;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            #region Validation
            {

            }
            #endregion Validation

            if (Input.AccessLevelId is not null)
            {
                var _Result = await _RoleApplication.GetRoleNamesByAccessLevelIdAsync(new InpGetRoleNamesByAccessLevelId
                {
                    AccessLevelId = Input.AccessLevelId
                });

                if (_Result.IsSuccess)
                    ViewData["SelectedRoles"] = _Result.Data;
                else
                    return BadRequest(_Result.Message);
            }
            return Page();
        }
        catch (ArgumentInvalidException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
            return StatusCode(500);
        }
        return Page();
    }

    public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request)
    {
        return Page();
    }
        
    [BindProperty(SupportsGet = true)]
    public viCompoListRoles Input { get; set; }
}
