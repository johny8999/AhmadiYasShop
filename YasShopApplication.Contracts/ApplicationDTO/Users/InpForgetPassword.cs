using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.ApplicationDTO.Users
{
    public class InpForgetPassword
    {
        [Display(Name = nameof(Email))]
        [RequiredString]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = nameof(ForgetPasswordUrl))]
        [RequiredString]
        [MaxLength(200)]
        public string ForgetPasswordUrl { get; set; }
    }
}
