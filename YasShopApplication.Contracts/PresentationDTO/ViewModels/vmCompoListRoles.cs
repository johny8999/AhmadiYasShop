using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.PresentationDTO.ViewModels;

public class vmCompoListRoles
{
    [Display(Name = nameof(Id))]
    public string Id { get; set; }
    
    public string ParentId { get; set; }

    [Display(Name = nameof(RoleName))]
    public string RoleName { get; set; }

    [Display(Name = nameof(Description))]
    public string Description { get; set; }

    public bool HasChild { get; set; }

    [Display(Name = nameof(PageName))]
    public string PageName { get; set; }

}
