using System.ComponentModel.DataAnnotations;
using Framework.Common.DataAnnotations.Strings;

namespace YasShop.Application.Contracts.PresentationDTO.input;

public sealed class InpLoginByEmailPasswordDto
{
    [Display(Name = nameof(Email))]
    [StringLength(100)]
    [Email]
    public string Email { get; set; }

    [Display(Name = nameof(Password))]
    [StringLength(100)]
    public string Password { get; set; }
}