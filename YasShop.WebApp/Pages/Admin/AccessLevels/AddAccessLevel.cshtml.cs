using Framework.Application.Services.Localizer;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using YasShop.Application.AccessLevel;
using YasShop.Application.Contracts.PresentationDTO.ViewInputs;

namespace YasShop.WebApp.Pages.Admin.AccessLevels
{
    public class AddAccessLevelModel : PageModel
    {
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILogger _Logger;
        private readonly IAccessLevelApplication _AccessLevelApplication;

        public AddAccessLevelModel(ILocalizer localizer, IServiceProvider serviceProvider, ILogger logger, IAccessLevelApplication accessLevelApplication)
        {
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
            _Logger = logger;
            _AccessLevelApplication = accessLevelApplication;
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

            }
            catch (Exception)
            {

                throw;
            }
        }

        [BindProperty]
        public viAddAccessLevel Input { get; set; }

    }

}
