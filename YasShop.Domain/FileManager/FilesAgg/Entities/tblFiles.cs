using Framework.Domain;
using System;
using YasShop.Domain.FileManager.FilePath.Entities;
using YasShop.Domain.FileManager.FileTypes.Entity;
using YasShop.Domain.Region.CountryAgg.Entity;

namespace YasShop.Domain.FileManager.FilesAgg.Entities
{
    public class tblFiles : IEntity
    {
        public Guid Id { get; set; }
        public Guid FilePathId { get; set; }
        public Guid FileTypeId { get; set; }
        public Guid? UserId { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public long SizeOnDisk { get; set; }
        public DateTime Date { get; set; }

        public virtual tblFilePath tblFilePath { get; set; }
        public virtual tblFileTypes tblFileTypes { get; set; }
        public virtual tblContries tblContries { get; set; }
    }
}
