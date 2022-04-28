using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YasShop.Domain.Region.LanguageAgg.Contract;
using YasShop.Domain.Region.LanguageAgg.Entities;
using YasShop.Infrastructure.EfCore.Context;

namespace YasShop.Infrastructure.EfCore.Repository.Languages
{
   public class LanguagesRepository: BaseRepository<tblLanguages>, ILanguagesRepository
    {
        public LanguagesRepository(MainContext context) : base(context)
        {

        }
    }
}
