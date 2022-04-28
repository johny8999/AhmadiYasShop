using Framework.Const;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Result;
using YasShop.Application.Contracts.ApplicationDTO.Users;
using YasShop.Application.Users;
using YasShop.WebApp.Common.ExMethod;

namespace YasShop.WebApp.Pages.Auth
{
    public class EmailConfirmModel : PageModel
    {
        private readonly IUserApplication _UserApplication;
        
        public EmailConfirmModel(IUserApplication userApplication)
        {
            _UserApplication = userApplication;
        }

        public async Task<IActionResult> OnGetAsync(string Token)
        {
            if (string.IsNullOrWhiteSpace(Token))
            {
                return BadRequest("Token can not be null");
            }
            var Result =await _UserApplication.EmailConfirmationAsync(new InpEmailConfirmation()
            {
                Token=Token
            });

            if ( Result.IsSuccess)
            {
                Response.DeleteAuthCookie();
                IsSuccess = true;

                return Page();
            }
            else
            {
                IsSuccess = false;
                return Page();
            }
        }

        public bool IsSuccess { get; set; }
    }
}
