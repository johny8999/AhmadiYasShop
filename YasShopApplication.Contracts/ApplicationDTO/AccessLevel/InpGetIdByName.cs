using Framework.Common.DataAnnotations.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YasShop.Application.Contracts.ApplicationDTO.AccessLevel
{
    public class InpGetIdByName
    {
        [Display]
        [RequiredString]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
