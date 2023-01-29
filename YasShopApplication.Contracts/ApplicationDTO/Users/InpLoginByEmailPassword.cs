using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.ApplicationDTO.Users
{
    public class InpLoginByEmailPassword
    {
        [Display(Name = nameof(Email))]
        [StringLength(100)]
        [Email]
        public string Email { get; set; }

        [Display(Name = nameof(Password))]
        [StringLength(100)]
        public string Password { get; set; }
    }
}
