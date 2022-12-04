using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.PresentationDTO.ViewInputs;

public class viCompoListRoles
{
    [Display(Name = (nameof(AccessLevelId)))]
    [GUID]
    public string AccessLevelId { get; set; }

}
