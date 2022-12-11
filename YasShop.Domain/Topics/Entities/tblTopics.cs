using Framework.Domain;
using System;
using System.Collections.Generic;
using YasShop.Domain.Category.Entities;
using YasShop.Domain.ProductAgg.Entity;

namespace YasShop.Domain.Topics.Entities
{
    public class tblTopics : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        public virtual ICollection<tblCategory> tblCategory { get; set; }
        public virtual ICollection<tblProducts> tblProducts { get; set; }
    }
}
