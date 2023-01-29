using Framework.Common.DataAnnotations.Strings;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.ApplicationDTO.AccessLevel
{
    public class InpEditAccessLevel
    {
        [Display(Name = nameof(Id))]
        [GUID]
        [RequiredString]
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
