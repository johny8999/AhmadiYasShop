using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.Endpoint.input
{
    public class InpLoginByPhoneNumberStep2
    {
        [Display(Name = "PhoneNumber")]
        [RequiredString]
        public string PhoneNumber { get; set; }

        [Display(Name = "OTPCode")]
        [RequiredString]
        public string OTPCode { get; set; }
    }
}
