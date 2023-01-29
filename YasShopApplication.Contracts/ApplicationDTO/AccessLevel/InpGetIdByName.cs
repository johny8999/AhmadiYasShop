using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.ApplicationDTO.AccessLevel
{
    public class InpGetIdByName
    {
        [Display]
        [RequiredString]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
