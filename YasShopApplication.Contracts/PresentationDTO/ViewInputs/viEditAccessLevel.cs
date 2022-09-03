using Framework.Common.DataAnnotations.Strings;
using System.Collections.Generic;
using System.ComponentModel;

namespace YasShop.Application.Contracts.PresentationDTO.ViewInputs
{
    public class viEditAccessLevel
    {
        [DisplayName(nameof(Name))]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [DisplayName(nameof(Name))]
        [RequiredString]
        [MaxLengthString(100)]
        public string Name { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
