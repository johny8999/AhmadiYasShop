using AutoMapper;
using Framework.Infrastructure;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using YasShop.Application.AccessLevel;
using YasShop.Application.Contracts.PresentationDTO.ViewInputs;
using YasShop.Application.Contracts.PresentationDTO.ViewModels;
using YasShop.WebApp.Common.Utilities.MessageBox;

namespace YasShop.WebApp.Pages.Admin.AccessLevels
{
    [Authorize]
    public class ListAccessLevelModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IMsgBox _MsgBox;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IAccessLevelApplication _AccessLevelApplication;

        public ListAccessLevelModel(ILogger logger, IMapper mapper, IMsgBox msgBox,
            IServiceProvider serviceProvider, IAccessLevelApplication accessLevelApplication)
        {
            _Logger = logger;
            _Mapper = mapper;
            _MsgBox = msgBox;
            _ServiceProvider = serviceProvider;
            _AccessLevelApplication = accessLevelApplication;
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

        }

        public viListAccessLevelModel input { get; set; }
        public vmListAccessLevelModel Data { get; set; }
    }
}
