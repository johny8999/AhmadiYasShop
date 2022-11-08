using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.ApplicationDTO.Users
{
    public class InpLogin
    {
        [Display(Name = nameof(UserId))]
        [StringLength(100)]
        public string UserId { get; set; }

        [Display(Name = nameof(Password))]
        [StringLength(100)]
        public string Password { get; set; }
    }
}
