using AutoMapper;
using Framework.Application.Exceptions;
using Framework.Application.Services.Localizer;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using YasShop.Application.AccessLevel;
using YasShop.Application.Contracts.ApplicationDTO.AccessLevel;
using YasShop.Application.Contracts.PresentationDTO.ViewInputs;
using YasShop.Application.Contracts.PresentationDTO.ViewModels;
using YasShop.WebApp.Common.Utilities.MessageBox;
using YasShop.WebApp.Localization;

namespace YasShop.WebApp.Pages.Admin.AccessLevels
{
    [Authorize]
    public class ListAccessLevelModel : PageModel
    {
        private readonly ILocalizer _localizer;
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IMsgBox _MsgBox;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IAccessLevelApplication _AccessLevelApplication;

        public ListAccessLevelModel(ILogger logger, IMapper mapper, IMsgBox msgBox,
            IServiceProvider serviceProvider, IAccessLevelApplication accessLevelApplication, ILocalizer localizer)
        {
            _Logger = logger;
            _Mapper = mapper;
            _MsgBox = msgBox;
            _ServiceProvider = serviceProvider;
            _AccessLevelApplication = accessLevelApplication;
            _localizer = localizer;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            return Page();
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest Request)
        {
            var qData = await _AccessLevelApplication.GetAccessLevelForAdminAsync(new InpGetAccessLevelForAdmin
            {
                Name = null,
                Page = (short)Request.Page,
                Take = (short)Request.PageSize
            });

            var DataGrid = qData.LstItems.ToDataSourceResult(Request);
            DataGrid.Total = (int)qData.PageData.CountAllItem;
            DataGrid.Data = qData.LstItems;
            return new JsonResult(DataGrid);
        }

        public async Task<IActionResult> OnPostDeleteAsync(viDeleteAccessLevel input)
        {
            try
            {
                #region Validation
                {
                    input.CheckModelState(_ServiceProvider);
                }
                #endregion Validation

                var _Result = await _AccessLevelApplication.DeleteAccessLevelAsync(new InputDeleteAccessLevel { Id = input.Id });
                if (!_Result.IsSuccess)
                    return _MsgBox.FailMsg(_localizer[_Result.Message]);

                return _MsgBox.SucssessMsg(_localizer[_Result.Message],"refreshGrid()");
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

        [BindProperty]
        public viListAccessLevelModel input { get; set; }
        public vmListAccessLevelModel Data { get; set; }
    }
}
