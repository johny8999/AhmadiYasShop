using Framework.Common.DataAnnotations.Strings;

namespace YasShop.Application.Contracts.ApplicationDTO.Users
{
    public class InpGetAllDetailsForUser
    {
        [RequiredString]
        [GUID]
        public string UserId { get; set; }
    }
}
