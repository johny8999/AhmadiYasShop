using Framework.Domain;
using System;
using System.Collections.Generic;
using YasShop.Domain.Region.CityAgg.Entity;
using YasShop.Domain.Region.CountryAgg.Entity;

namespace YasShop.Domain.Region.ProvinceAgg.Entity
{
    public class tblProvince : IEntity
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<tblCities> tblCities { get; set; }
        public virtual tblContries tblContries { get; set; }
    }
}
