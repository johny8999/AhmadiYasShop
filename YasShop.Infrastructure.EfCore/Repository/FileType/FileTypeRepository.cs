using YasShop.Domain.FileManager.FileTypes.Contracts;
using YasShop.Domain.FileManager.FileTypes.Entity;
using YasShop.Infrastructure.EfCore.Context;

namespace YasShop.Infrastructure.EfCore.Repository.FileType
{
    public class FileTypeRepository : BaseRepository<tblFileTypes>, IFileTypeRepository
    {
        public FileTypeRepository(MainContext Context) : base(Context)
        {

        }
    }
}
