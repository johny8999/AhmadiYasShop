using Framework.Common.DataAnnotations.Strings;

namespace YasShop.Application.Contracts.ApplicationDTO.Role
{
    public class InpGetIdByRoleName
    {
        [RequiredString]
        [GUID]
        public string RoleName { get; set; }
    }
}
