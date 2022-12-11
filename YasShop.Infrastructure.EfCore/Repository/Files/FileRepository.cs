using YasShop.Domain.FileManager.Files.Contracts;
using YasShop.Domain.FileManager.FilesAgg.Entities;
using YasShop.Infrastructure.EfCore.Context;

namespace YasShop.Infrastructure.EfCore.Repository.Files
{
    public class FileRepository : BaseRepository<tblFiles>, IFileRepository
    {
        public FileRepository(MainContext Context) : base(Context)
        {

        }
    }
}
