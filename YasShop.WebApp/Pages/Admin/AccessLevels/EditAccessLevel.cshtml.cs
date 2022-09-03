using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using YasShop.Application.Contracts.PresentationDTO.ViewInputs;

namespace YasShop.WebApp.Pages.Admin.AccessLevels
{
    public class EditAccessLevelModel : PageModel
    {
        public Task<IActionResult> OnGetAsync(viGetEditAccessLevel Input)
        {
        }

        public viEditAccessLevel Input { get; set; }
    }
}
