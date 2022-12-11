using YasShop.Domain.FileManager.FilePath.Contracts;
using YasShop.Domain.FileManager.FilePath.Entities;
using YasShop.Infrastructure.EfCore.Context;

namespace YasShop.Infrastructure.EfCore.Repository.FilePath
{
    public class FilePathRepository : BaseRepository<tblFilePath>, IFilePathRepository
    {
        public FilePathRepository(MainContext Context) : base(Context)
        {

        }
    }
}
