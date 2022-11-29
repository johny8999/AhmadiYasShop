using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.ApplicationDTO.Users
{
    public class InpResendOtpCode
    {
        [Display(Name = "PhoneNumber")]
        [RequiredString]
        public string PhoneNumber { get; set; }
    }
}
