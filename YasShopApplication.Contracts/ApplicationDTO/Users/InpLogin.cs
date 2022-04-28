using Framework.Common.DataAnnotations.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YasShop.Application.Contracts.ApplicationDTO.Users
{
    public class InpLogin
    {
        [Display(Name =nameof(UserId))]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        [Display(Name =nameof(Password))]
        [RequiredString]
        public string Password { get; set; }
    }
}
