using Framework.Domain;
using System;
using System.Collections.Generic;
using YasShop.Domain.FileManager.FilePath.Entities;

namespace YasShop.Domain.FileManager.FileServers.Entities
{
    public class tblFileServer : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HttpDomain { get; set; }
        public string HttpPath { get; set; }
        public long Capacity { get; set; }
        public string FtpData { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<tblFilePath> tblFilePath { get; set; }
    }
}
