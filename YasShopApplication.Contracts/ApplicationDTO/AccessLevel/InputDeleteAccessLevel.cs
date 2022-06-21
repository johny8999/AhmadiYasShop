using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.ApplicationDTO.AccessLevel
{
    public class InputDeleteAccessLevel
    {
        [Display(Name = nameof(Id))]
        [RequiredString]
        [GUID]
        public string Id { get; set; }
    }
}
