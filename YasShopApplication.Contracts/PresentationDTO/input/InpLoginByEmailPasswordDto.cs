using System.ComponentModel.DataAnnotations;
using Framework.Common.DataAnnotations.Strings;

namespace YasShop.Application.Contracts.PresentationDTO.input;

public sealed class InpLoginDto
{
    [Display(Name =nameof(UserId))]
    [RequiredString]
    [GUID]
    public string UserId { get; set; }

    [Display(Name =nameof(Password))]
    [RequiredString]
    public string Password { get; set; }
}