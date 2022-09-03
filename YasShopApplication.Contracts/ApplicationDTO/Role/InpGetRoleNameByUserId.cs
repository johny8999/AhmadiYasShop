using Framework.Common.DataAnnotations.Strings;

namespace YasShop.Application.Contracts.ApplicationDTO.Role
{
    public class InpGetRoleNameByUserId
    {
        [RequiredString]
        [GUID]
        public string UserId { get; set; }
    }
}
