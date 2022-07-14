using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using YasShop.Application.Contracts.PresentationDTO.ViewInput;

namespace YasShop.WebApp.Pages.Admin.AccessLevels.Components;

[Authorize]
public class Compo_ListRolesModel : PageModel
{
    public async Task<IActionResult> OnGetAsync()
    {
        return Page();
    }

    public viCompoListRoles Input { get; set; }
}
