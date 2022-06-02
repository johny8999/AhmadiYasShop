using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.ApplicationDTO.AccessLevel
{
    public class InpGetAccessLevelForAdmin
    {
        [Display (Name =nameof(Name))]
        [MaxLengthString(150)]
        public string Name { get; set; }

        [Display(Name = nameof(Page))]
        [Range(1,short.MaxValue,ErrorMessage ="RangeMsg")]
        public short Page { get; set; }

        [Display(Name = nameof(Take))]
        [Range(1,short.MaxValue,ErrorMessage = "RangeMsg")]
        public short Take { get; set; }
    }
}
