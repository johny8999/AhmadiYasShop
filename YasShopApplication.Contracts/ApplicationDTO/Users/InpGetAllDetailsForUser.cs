using Framework.Common.DataAnnotations.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YasShop.Application.Contracts.ApplicationDTO.Users
{
    public class InpGetAllDetailsForUser
    {
        [RequiredString]
        [GUID]
        public string UserId { get; set; }
    }
}
