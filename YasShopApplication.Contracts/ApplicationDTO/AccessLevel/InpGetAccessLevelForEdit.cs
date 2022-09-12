using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.ApplicationDTO.AccessLevel
{
    public class InpGetAccessLevelForEdit
    {
        [Display(Name =nameof(Id))]
        [GUID]
        [RequiredString]
        public string Id { get; set; }

    }
}
