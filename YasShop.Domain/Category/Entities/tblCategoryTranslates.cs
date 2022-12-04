using Framework.Domain;
using System;
using YasShop.Domain.Region.LanguageAgg.Entities;

namespace YasShop.Domain.Category.Entities
{
    public class tblCategoryTranslates : IEntity
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid LangId { get; set; }
        public Guid? ImgId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual tblCategory tblCategory { get; set; }
        public virtual tblLanguages tblLanguages { get; set; }
    }
}
