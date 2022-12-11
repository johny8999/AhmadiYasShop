using Framework.Domain;
using System;
using System.Collections.Generic;
using YasShop.Domain.FileManager.FilesAgg.Entities;

namespace YasShop.Domain.FileManager.FileTypes.Entity
{
    public class tblFileTypes : IEntity
    {
        public Guid Id { get; set; }
        public string MimeType { get; set; }
        public string Extentions { get; set; }

        public virtual ICollection<tblFiles> tblFiles { get; set; }
    }
}
