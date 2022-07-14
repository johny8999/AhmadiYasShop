using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.PresentationDTO.ViewInput;

public class viCompoListRoles
{
    [Display(Name = (nameof(AccessLevelId)))]
    [RequiredString]
    [GUID]
    public string AccessLevelId { get; set; }

}
