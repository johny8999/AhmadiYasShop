using Framework.Domain;
using System;
using System.Collections.Generic;
using YasShop.Domain.FileManager.FilesAgg.Entities;
using YasShop.Domain.Region.ProvinceAgg.Entity;

namespace YasShop.Domain.Region.CountryAgg.Entity
{
    public class tblContries : IEntity
    {
        public Guid Id { get; set; }
        public Guid FlagImgId { get; set; }
        public string Name { get; set; }
        public string PhoneCode { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<tblProvince> tblProvince { get; set; }
        public virtual tblFiles tblFiles { get; set; }
    }
}
