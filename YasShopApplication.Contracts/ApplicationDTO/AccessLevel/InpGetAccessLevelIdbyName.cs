using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.ApplicationDTO.AccessLevel
{
    public class InpGetAccessLevelIdbyName
    {
        [Display(Name = nameof(AccessLevelName))]
        [RequiredString]
        [StringLength(150)]
        public string AccessLevelName { get; set; }
    }
}
