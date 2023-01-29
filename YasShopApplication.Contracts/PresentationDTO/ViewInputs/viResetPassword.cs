using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.PresentationDTO.ViewInputs
{
    public class viResetPassword
    {
        [Display(Name = nameof(Token))]
        [RequiredString]
        public string Token { get; set; }

        [Display(Name = nameof(Password))]
        [RequiredString]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = nameof(RePassword))]
        [RequiredString]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password mismatch")]
        public string RePassword { get; set; }
    }
}
