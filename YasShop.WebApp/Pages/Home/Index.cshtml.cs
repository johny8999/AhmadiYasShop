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
              
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
            }
        }
    }
}
