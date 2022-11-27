using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.ApplicationDTO.Users
{
    public class InpLoginByPhoneNumberStep1
    {
        [Display(Name ="PhoneNumber")]
        [RequiredString]
        //[PhoneNumber]
        public string PhoneNumber { get; set; }
    }
}
