﻿using Framework.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.ApplicationDTO.Role;

public class InpGetAllRolesByParentId
{
    [Display (Name =nameof(ParentId))]
    [RequiredString]
    [GUID]
    public string ParentId { get; set; }
}
