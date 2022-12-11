using Framework.Domain;
using System;
using YasShop.Domain.Region.CityAgg.Entity;
using YasShop.Domain.Users.UserAgg.Entities;

namespace YasShop.Domain.Users.AddressAgg.Entity
{
    public class tblAddress : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CityId { get; set; }
        public string District { get; set; } // محله
        public string Address { get; set; }
        public string Plaque { get; set; }
        public string Unit { get; set; }
        public string PostalCode { get; set; }
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Date { get; set; }

        public virtual tblUsers tblUsers { get; set; }
        public virtual tblCities tblCities { get; set; }
    }
}
