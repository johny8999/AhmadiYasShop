using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.Endpoint.input
{
    public class InpChangeAccLevel
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "AccLevelId")]
        [RequiredString]
        [GUID]
        public string AccLevelId { get; set; }
    }
}
