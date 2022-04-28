using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.ApplicationDTO.Users
{
    public class InpEmailConfirmation
    {
        [Display(Name = "Token")]
        [RequiredString]
        public string Token { get; set; }

    }
}
