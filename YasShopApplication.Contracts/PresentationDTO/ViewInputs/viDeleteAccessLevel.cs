using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.PresentationDTO.ViewInputs
{
    public class viDeleteAccessLevel
    {
        [Display(Name = nameof(Id))]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

    }
}
