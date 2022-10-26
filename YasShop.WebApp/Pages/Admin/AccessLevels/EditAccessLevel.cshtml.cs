using AutoMapper;
using Framework.Application.Exceptions;
using Framework.Application.Services.Localizer;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using YasShop.Application.AccessLevel;
using YasShop.Application.Contracts.ApplicationDTO.AccessLevel;
using YasShop.Application.Contracts.PresentationDTO.ViewInputs;
using YasShop.WebApp.Common.Utilities.MessageBox;

namespace YasShop.WebApp.Pages.Admin.AccessLevels
{
    public class EditAccessLevelModel : PageModel
    {
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly IMsgBox _MsgBox;
        private readonly IAccessLevelApplication _AccessLevelApplication;
        public EditAccessLevelModel(ILogger logger, IMapper mapper, ILocalizer localizer, IServiceProvider serviceProvider, IMsgBox msgBox, IAccessLevelApplication accessLevelApplication)
        {
            _Logger = logger;
            _Mapper = mapper;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
            _MsgBox = msgBox;
            _AccessLevelApplication = accessLevelApplication;
        }

        public async Task<IActionResult> OnGetAsync(viGetEditAccessLevel Input)
        {
            try
            {
                #region Validation
                {
                    Input.CheckModelState(_ServiceProvider);
                }
                #endregion Validation

                var Result = await _AccessLevelApplication
                    .GetAccessLevelForEditAsync(Input.Adapt<InpGetAccessLevelForEdit>());

                if (!Result.IsSuccess)
                    return StatusCode(400);

                this.Input = Result.Data.Adapt<viEditAccessLevel>();

                return Page();
            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.FailDefultMsg();
            }
            catch (System.Exception ex)
            {
                _Logger.Error(ex.Message);
                return _MsgBox.FailDefultMsg();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                #region Validation
                {
                    Input.CheckModelState(_ServiceProvider);
                }
                #endregion Validation

                return Page();
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        [BindProperty]
        public viEditAccessLevel Input { get; set; }
    }
}
