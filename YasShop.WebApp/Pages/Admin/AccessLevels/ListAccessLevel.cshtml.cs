using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace YasShop.WebApp.Pages.Admin.AccessLevels
{
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

        public int MyProperty { get; set; }
    }
}
