using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using YasShop.Application.Contracts.PresentationDTO.ViewInputs;

namespace YasShop.WebApp.Pages.Admin.AccessLevels
{
    public class AddAccessLevelModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            return Page();
        }

        [BindProperty]
        public viAddAccessLevel Input { get; set; }

    }

}
