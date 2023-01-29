using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.ApplicationDTO.Languages
{
    public class InpIsValidAbbrForSiteLang
    {
        [Display(Name = "Abbr")]
        [RequiredString]
        [MaxLengthString(10)]
        public string Abbr { get; set; }
    }
}
