using Framework.Application.Exceptions;
using Framework.Application.Services.Localizer;
using Framework.Infrastructure;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Role;
using YasShop.Application.Contracts.PresentationDTO.ViewInput;
using YasShop.Application.Roles;
using YasShop.WebApp.Common.Utilities.MessageBox;

namespace YasShop.WebApp.Pages.Admin.AccessLevels.Components;

[Authorize]
public class Compo_ListRolesModel : PageModel
{
    private readonly ILogger _Logger;
    private readonly ILocalizer _Localizer;
    private readonly IServiceProvider _ServiceProvider;
    private readonly IRoleApplication _RoleApplication;
    private readonly IMsgBox _MsgBox;

    public Compo_ListRolesModel(IRoleApplication roleApplication, ILogger logger, IServiceProvider serviceProvider, ILocalizer localizer, IMsgBox msgBox)
    {
        _RoleApplication = roleApplication;
        _Logger = logger;
        _ServiceProvider = serviceProvider;
        _Localizer = localizer;
        _MsgBox = msgBox;
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

    public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request, string ParentId)
    {
        try
        {
            #region Validation
            {

            }
            #endregion Validation

            var qAllRolesByParentIdData = await _RoleApplication.GetAllRolesByParentIdAsync(new InpGetAllRolesByParentId
            {
                ParentId = ParentId
            });

            if (!qAllRolesByParentIdData.IsSuccess)
                return _MsgBox.FailMsg(qAllRolesByParentIdData.Message);

            var DataGrid = qAllRolesByParentIdData.Data.ToDataSourceResult(request);
            DataGrid.Total = qAllRolesByParentIdData.Data.Count;
            DataGrid.Data = qAllRolesByParentIdData.Data;
            return new JsonResult(DataGrid);

        }
        catch (ArgumentInvalidException ex)
        {
            return BadRequest();
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
            return StatusCode(500);
        }
    }

    [BindProperty(SupportsGet = true)]
    public viCompoListRoles Input { get; set; }
}
