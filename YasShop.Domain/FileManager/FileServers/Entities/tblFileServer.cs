using Framework.Domain;
using System;

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
    }
}
