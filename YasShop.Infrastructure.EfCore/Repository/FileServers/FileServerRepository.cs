using YasShop.Domain.FileManager.FileServers.Contracts;
using YasShop.Domain.FileManager.FileServers.Entities;
using YasShop.Infrastructure.EfCore.Context;

namespace YasShop.Infrastructure.EfCore.Repository.FileServers
{
    public class FileServerRepository : BaseRepository<tblFileServer>, IFileServerRepository
    {
        public FileServerRepository(MainContext Context) : base(Context)
        {

        }
    }
}
