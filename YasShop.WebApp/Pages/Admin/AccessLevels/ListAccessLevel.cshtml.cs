using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using YasShop.Application.Contracts.PresentationDTO.ViewInputs;
using YasShop.Application.Contracts.PresentationDTO.ViewModels;

namespace YasShop.WebApp.Pages.Admin.AccessLevels
{
    [Authorize]
    public class ListAccessLevelModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return Page();
        }

        public viListAccessLevelModel input { get; set; }
        public vmListAccessLevelModel  Data { get; set; }
    }
}
