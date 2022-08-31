using Framework.Application.Exceptions;
using Framework.Application.Services.Localizer;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
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
    public class AddAccessLevelModel : PageModel
    {
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly IAccessLevelApplication _AccessLevelApplication;

        public AddAccessLevelModel(ILocalizer localizer, IServiceProvider serviceProvider, ILogger logger, IAccessLevelApplication accessLevelApplication, IMsgBox msgBox)
        {
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
            _Logger = logger;
            _AccessLevelApplication = accessLevelApplication;
            _MsgBox = msgBox;
        }

        public IActionResult OnGet()
        {
            return Page();
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

                var Result = await _AccessLevelApplication.AddAccessLevelAsync(new InpAddAccessLevel
                {
                    Name = Input.Name,
                    Roles = Input.Roles
                });

                if (!Result.IsSuccess)
                    return _MsgBox.FailMsg(Result.Message);

                return _MsgBox.SucssessDefultMsg();

            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.FailMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex, ex.Message);
                return _MsgBox.FailDefultMsg();
            }
        }

        [BindProperty]
        public viAddAccessLevel Input { get; set; }

    }

}
