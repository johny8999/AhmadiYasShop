using Framework.Application.Exceptions;
using Framework.Application.Services.Localizer;
using Framework.Common.ExMethods;
using Framework.Const;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Globalization;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Users;
using YasShop.Application.Contracts.PresentationDTO.ViewInputs;
using YasShop.Application.Users;
using YasShop.WebApp.Common.Utilities.MessageBox;

namespace YasShop.WebApp.Pages.Auth.RecoveryPassword
{
    public class ForgetPasswordModel : PageModel
    {
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILogger _Logger;
        private readonly IUserApplication _UserApplication;
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        public ForgetPasswordModel(IServiceProvider serviceProvider, ILogger logger,
            IUserApplication userApplication, IMsgBox msgBox, ILocalizer localizer)
        {
            _ServiceProvider = serviceProvider;
            _Logger = logger;
            _UserApplication = userApplication;
            _MsgBox = msgBox;
            _Localizer = localizer;
        }

        public IActionResult OnGet(string ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/Auth/LogIn";
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

                var _Result = await _UserApplication.ForgetPasswordAsync(new InpForgetPassword()
                {
                    Email = Input.Email,
                    ForgetPasswordUrl = $"{SiteSettingConst.SiteUrl}/{CultureInfo.CurrentCulture.Parent.Name}/Auth/ResetPassword?Token=[TOKEN]"
                });

                if (_Result.IsSuccess)
                    return _MsgBox.SucssessMsg(_Localizer[_Result.Message], "goToReturnUrl()");

                else
                    return _MsgBox.FailDefultMsg(_Localizer[_Result.Message]);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return _MsgBox.ModelStateMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.FailMsg(_Localizer["Error500"]);
            }
        }

        [BindProperty]
        public viForgetPassword Input { get; set; }
    }
}
