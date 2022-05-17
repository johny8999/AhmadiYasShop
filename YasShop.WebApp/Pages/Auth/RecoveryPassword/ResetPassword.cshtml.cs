using Framework.Application.Exceptions;
using Framework.Application.Services.Localizer;
using Framework.Common.ExMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Globalization;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Result;
using YasShop.Application.Contracts.ApplicationDTO.Users;
using YasShop.Application.Contracts.PresentationDTO.ViewInputs;
using YasShop.Application.Users;
using YasShop.WebApp.Common.Utilities.MessageBox;

namespace YasShop.WebApp.Pages.Auth.RecoveryPassword
{
    public class ResetPasswordModel : PageModel
    {
        private readonly IServiceProvider _ServiceProvider;
        private readonly IUserApplication _UserApplication;
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        public ResetPasswordModel(IServiceProvider serviceProvider, IUserApplication userApplication, IMsgBox msgBox, ILocalizer localizer)
        {
            _ServiceProvider = serviceProvider;
            _UserApplication = userApplication;
            _MsgBox = msgBox;
            _Localizer = localizer;
        }

        public IActionResult OnGet(string ReturnUrl=null,string Token=null)
        {
            ViewData["ReturnUrl"] = ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/Auth/LogIn";
            Input = new viResetPassword()
            {
                Token = Token
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                #region Validation
               Input.CheckModelState(_ServiceProvider);
                #endregion Validation

                var _Result = await _UserApplication.ResetPasswordAsync(new InpResetPassword()
                {
                    Password=Input.Password,
                    RePassword=Input.RePassword,
                    Token=Input.Token
                });

                if (_Result.IsSuccess)
                    return _MsgBox.SucssessMsg(_Localizer[_Result.Message], "goToLogin()");

                else
                    return _MsgBox.FailMsg(_Localizer[_Result.Message]);
            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.FailMsg(ex.Message);
            }
        }
        [BindProperty]
        public viResetPassword Input { get; set; }
    }
}
