using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.ApplicationDTO.Users
{
    public class InpChangeUserAccessLevel
    {
        [Display(Name = nameof(UserId))]
        [RequiredString]
        [StringLength(450)]
        public string UserId { get; set; }

        [Display(Name = nameof(AccessLevelId))]
        [RequiredString]
        [StringLength(150)]
        public string AccessLevelId { get; set; }
    }
}
