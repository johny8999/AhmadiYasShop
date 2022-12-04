using Framework.Common.DataAnnotations.Strings;

namespace YasShop.Application.Contracts.Endpoint.input;

public class InpRefreshLogInDto
{
    [RequiredString]
    [GUID]
    public string UserId { get; set; }
}