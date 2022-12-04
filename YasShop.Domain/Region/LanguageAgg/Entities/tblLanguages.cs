using Framework.Domain;
using System;
using System.Collections.Generic;
using YasShop.Domain.Category.Entities;

namespace YasShop.Domain.Region.LanguageAgg.Entities
{
    public class tblLanguages : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Abbr { get; set; }
        public string NativeName { get; set; }
        public bool IsRtl { get; set; }
        public bool IsActive { get; set; }
        public bool UseForSiteLanguage { get; set; }

        public virtual ICollection<tblCategoryTranslates> tblCategoryTranslates { get; set; }
    }
}
