﻿using System.ComponentModel.DataAnnotations;

namespace YasShop.Application.Contracts.PresentationDTO.ViewModels
{
    public class vmListAccessLevelModel
    {
        public string Id { get; set; }

        [Display(Name = nameof(Name))]
        public string Name { get; set; }

        [Display(Name = nameof(CountUser))]
        public int CountUser { get; set; }

    }
}
