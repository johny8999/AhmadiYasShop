using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

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
