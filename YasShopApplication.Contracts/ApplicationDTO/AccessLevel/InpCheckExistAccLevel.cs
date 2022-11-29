using Framework.Common.DataAnnotations.Strings;

namespace YasShop.Application.Contracts.ApplicationDTO.AccessLevel
{
    public class InpCheckExistAccLevel
    {
        [RequiredString]
        [GUID]
        public string AccLevelId { get; set; }
    }
}
