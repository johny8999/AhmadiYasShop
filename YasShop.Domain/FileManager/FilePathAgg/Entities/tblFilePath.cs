using Framework.Domain;
using System;
using System.Collections.Generic;
using YasShop.Domain.FileManager.FilesAgg.Entities;
using YasShop.Domain.FileManager.FileServers.Entities;

namespace YasShop.Domain.FileManager.FilePath.Entities
{
    public class tblFilePath : IEntity
    {
        public Guid Id { get; set; }
        public Guid FileServerId { get; set; }
        public string Path { get; set; }

        public virtual tblFileServer tblFileServer { get; set; }
        public virtual ICollection<tblFiles> tblFiles { get; set; }
    }
}
