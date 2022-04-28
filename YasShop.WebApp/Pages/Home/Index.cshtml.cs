using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace YasShop.WebApp.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly ILogger _Logger;

        public IndexModel(ILogger logger)
        {
            _Logger = logger;
        }

        public void OnGet()
        {
            try
            {
                int a = 10;
                int b = 0;
                int c = a / b;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
            }
        }
    }
}
