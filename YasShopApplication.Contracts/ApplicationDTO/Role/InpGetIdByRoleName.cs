using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.ApplicationDTO.Role
{
    public class InpGetIdByRoleName
    {
        [Display(Name =nameof(RoleName))]
        [RequiredString]
        [MaxLengthString(100)]
        public string RoleName { get; set; }
    }
}
