using Framework.Common.DataAnnotations.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YasShop.Application.Contracts.ApplicationDTO.Users
{
    public class OutIGetAllDetailsForUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string AccessLevelId { get; set; }
        public string AccessLevelTitle { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }

    }
}
