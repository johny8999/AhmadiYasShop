using YasShop.Domain.Category.Contracts;
using YasShop.Domain.Category.Entities;
using YasShop.Infrastructure.EfCore.Context;

namespace YasShop.Infrastructure.EfCore.Repository.Category
{
    public class CategoryRepository : BaseRepository<tblCategory>, ICategoryRepository
    {
        public CategoryRepository(MainContext Context) : base(Context)
        {

        }
    }
}
