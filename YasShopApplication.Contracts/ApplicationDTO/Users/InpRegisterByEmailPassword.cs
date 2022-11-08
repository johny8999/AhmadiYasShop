using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.ApplicationDTO.Users
{
    public class InpRegisterByEmailPassword
    {
        [Display(Name = "FullName")]
        [RequiredString]
        [MaxLengthString(100)]
        [FullName]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [RequiredString]
        [MaxLengthString(100)]
        [Email]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [RequiredString]
        [MaxLengthString(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        /// <summary>
        /// getting parameter with [TOKEN] variable
        /// </summary>
        [Display(Name = "ConfirmationLinkTemplate")]
        [RequiredString]
        [MaxLengthString(500)]
        public string ConfirmationLinkTemplate { get; set; }
    }
}
