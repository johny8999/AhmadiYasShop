using Framework.Common.DataAnnotations.Strings;
using System.Collections.Generic;
using System.ComponentModel;

namespace YasShop.Application.Contracts.PresentationDTO.ViewInputs
{
    public class viAddAccessLevel
    {
        [DisplayName(nameof(Name))]
        [RequiredString]
        [MaxLengthString(100)]
        public string Name { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
