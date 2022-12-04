using Framework.Domain;
using System;
using System.Collections.Generic;
using YasShop.Domain.Topics.Entities;

namespace YasShop.Domain.Category.Entities
{
    public class tblCategory : IEntity
    {
        public Guid Id { get; set; }
        public Guid? TopicId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<tblCategoryTranslates> tblCategoryTranslates { get; set; }
        public virtual tblTopics tblTopics { get; set; }
    }
}
