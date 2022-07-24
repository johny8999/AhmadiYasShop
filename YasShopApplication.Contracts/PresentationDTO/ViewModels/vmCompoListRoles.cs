namespace YasShop.Application.Contracts.PresentationDTO.ViewModels;

public class vmCompoListRoles
{
    public string Id { get; set; }
    public string ParentId { get; set; }
    public string RoleName { get; set; }
    public string Description { get; set; }
    public bool HasChild { get; set; }
    public string PageName { get; set; }

}
