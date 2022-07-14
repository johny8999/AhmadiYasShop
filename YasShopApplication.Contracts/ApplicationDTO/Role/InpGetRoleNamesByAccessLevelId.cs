using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.ApplicationDTO.Role;

public class InpGetRoleNamesByAccessLevelId
{
    [Display(Name = (nameof(AccessLevelId)))]
    [RequiredString]
    [GUID]
    public string AccessLevelId { get; set; }
}
