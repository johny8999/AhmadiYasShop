using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Framework.Application.Exceptions;
using Framework.Application.Services.Localizer;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YasShop.Application.Contracts.ApplicationDTO.Users;
using YasShop.Application.Contracts.PresentationDTO.ViewInputs;
using YasShop.Application.Users;
using YasShop.WebApp.Common.Utilities.MessageBox;

namespace YasShop.WebApp.Pages.Auth.LogIn
{
    public class LogInModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IMsgBox _MsgBox;
        private readonly IUserApplication _UserApplication;
        private readonly IMapper _Mapper;
        public LogInModel(ILocalizer localizer, IServiceProvider serviceProvider, ILogger logger, IMsgBox msgBox, IUserApplication userApplication, IMapper mapper)
        {
            _localizer = localizer;
            _ServiceProvider = serviceProvider;
            _Logger = logger;
            _MsgBox = msgBox;
            _UserApplication = userApplication;
            _Mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                #region Validation
                input.CheckModelState(_ServiceProvider);
                #endregion Validation

                var _MappedData =_Mapper.Map<InpLoginByEmailPassword>(input);
                var _Result=await _UserApplication.LoginByEmailPasswordAsync(_MappedData);

                if (_Result.IsSuccess)
                    return _MsgBox.SucssessMsg(_localizer[_Result.Message]);
                else
                    return _MsgBox.FailMsg(_localizer[_Result.Message]);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex.Message);
                return _MsgBox.ModelStateMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.ModelStateMsg(_localizer["Error500"]);
            }
        }
        [BindProperty]
        public viLogin input { get; set; }
    }
}
