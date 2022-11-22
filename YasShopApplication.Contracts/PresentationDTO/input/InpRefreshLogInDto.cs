using Framework.Common.DataAnnotations.Strings;

namespace YasShop.Application.Contracts.PresentationDTO.input;

public class InpRefreshLogInDto
{
    [RequiredString]
    [GUID]
    public string UserId { get; set; }
}