using YasShop.Domain.Category.Contracts;
using YasShop.Domain.Category.Entities;
using YasShop.Infrastructure.EfCore.Context;

namespace YasShop.Infrastructure.EfCore.Repository.Category
{
    public class CategoryTranslateRepository : BaseRepository<tblCategoryTranslates>, ICategoryTranslateRepository
    {
        public CategoryTranslateRepository(MainContext Context) : base(Context)
        {

        }
    }
}
