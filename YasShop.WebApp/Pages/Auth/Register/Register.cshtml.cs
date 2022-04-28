using Framework.Application.Services.Email;
using Framework.Application.Services.Localizer;
using Framework.Common.ExMethods;
using Framework.Const;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Users;
using YasShop.Application.Contracts.PresentationDTO.ViewInputs;
using YasShop.Application.Users;
using YasShop.WebApp.Common.Utilities.MessageBox;

namespace YasShop.WebApp.Pages.Auth.Register
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly IServiceProvider _Service;
        private readonly ILocalizer _Localizer;
        private readonly IUserApplication _UserApplication;


        public RegisterModel(ILogger logger, IMsgBox msgBox,
            IServiceProvider service,
            ILocalizer localizer,
            IUserApplication userApplication)
        {
            _Logger = logger;
            _MsgBox = msgBox;
            _Service = service;
            _Localizer = localizer;
            _UserApplication = userApplication; 
        }

        public IActionResult OnGet(string ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/Auth/Login";

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                Input.CheckModelState(_Service);

                var _result = await _UserApplication.RegisterByEmailPasswordAsync(new InpRegisterByEmailPassword()
                {
                    Email = Input.Email,
                    FullName = Input.FullName,
                    Password = Input.Password,
                    ConfirmationLinkTemplate= $"{SiteSettingConst.SiteUrl}/{CultureInfo.CurrentCulture.Parent.Name}/Auth/EmailConfirm?Token=[TOKEN]"
                });

                if (_result.IsSuccess)
                {
                    return _MsgBox.SucssessMsg(_result.Message,
                        $"location.href='{Url.Page("~/Pages/Auth/Login.cshtml", new { culture = CultureInfo.CurrentCulture.Parent.Name })}'");
                }
                else
                {
                    return _MsgBox.FailMsg(_result.Message);
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                // _Service.GetService<ILogger>().Error(ex);
                return _MsgBox.FailDefultMsg(_Localizer["Error500"]);
            }

        }

        //[BindProperty(SupportsGet =true)] //for get in worked
        [BindProperty]
        public viRegister Input { get; set; }
    }

}
