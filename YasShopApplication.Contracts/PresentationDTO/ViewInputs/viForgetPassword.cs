using Framework.Common.DataAnnotations.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YasShop.Application.Contracts.PresentationDTO.ViewInputs
{
    public class viForgetPassword
    {
        [Display(Name =nameof(Email))]
        [RequiredString]
        [EmailAddress]
        public string Email { get; set; }
    }
}
