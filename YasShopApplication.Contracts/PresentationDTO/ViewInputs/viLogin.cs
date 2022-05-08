using Framework.Common.DataAnnotations.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YasShop.Application.Contracts.PresentationDTO.ViewInputs
{
    public class viLogin
    {
        [Display(Name =nameof(Email))]
        [StringLength(100)]
        [Email]
        public string Email { get; set; }

        [Display(Name = nameof(Password))]
        [StringLength(100)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
