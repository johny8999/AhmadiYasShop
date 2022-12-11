using Framework.Domain;
using System;
using YasShop.Domain.Category.Entities;
using YasShop.Domain.Region.LanguageAgg.Entities;
using YasShop.Domain.Topics.Entities;
using YasShop.Domain.Users.UserAgg.Entities;

namespace YasShop.Domain.ProductAgg.Entity
{
    public class tblProducts : IEntity
    {
        public Guid Id { get; set; }
        public Guid? TopicId { get; set; }
        public Guid AuthorUserId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? ProductGroupId { get; set; } // درصد کارمزد از فروشنده
        public Guid LangId { get; set; }
        public DateTime Date { get; set; }
        public string UniqueNumber { get; set; } // Uniqe Number
        public string Name { get; set; } // Uniqe Name
        public string Title { get; set; }
        public string Description { get; set; }
        public ProductStatusEnum Status { get; set; }
        public string MetaTagKeyword { get; set; }
        public string MetaTagCanonical { get; set; }
        public string MetaTagDescreption { get; set; }

        public bool Incomplete { get; set; }
        public string IncompleteReason { get; set; }

        public virtual tblTopics tblTopics { get; set; }
        public virtual tblUsers tblUsers { get; set; }
        public virtual tblCategory tblCategory { get; set; }
        public virtual tblLanguages tblLanguages { get; set; }
    }

    public enum ProductStatusEnum
    {
        ItsForConfirm,
        IsConfirmed,
        IsDraft,
        IsDelete
    }
}
