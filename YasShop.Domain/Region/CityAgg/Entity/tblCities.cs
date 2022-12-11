using Framework.Domain;
using System;
using System.Collections.Generic;
using YasShop.Domain.Region.ProvinceAgg.Entity;
using YasShop.Domain.Users.AddressAgg.Entity;

namespace YasShop.Domain.Region.CityAgg.Entity
{
    public class tblCities : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProvinceId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual tblProvince tblProvince { get; set; }
        public virtual ICollection<tblAddress> tblAddress { get; set; }

    }
}
