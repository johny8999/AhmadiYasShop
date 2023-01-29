using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.ApplicationDTO.AccessLevel
{
    public class InpGetUserRoleByAccessId
    {
        [Display(Name =nameof(AccessLevelId))]
        [RequiredString]
        [StringLength(150)]
        public string AccessLevelId { get; set; }

    }
}
