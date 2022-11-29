using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.PresentationDTO.input
{
    public class InpResendSmsOtpCode
    {
        [Display(Name = "PhoneNumber")]
        [RequiredString]
        public string PhoneNumber { get; set; }
    }
}
