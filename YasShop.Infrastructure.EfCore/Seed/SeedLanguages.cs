using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YasShop.Domain.Region.LanguageAgg.Entities;

namespace YasShop.Infrastructure.EfCore.Seed
{
    public class SeedLanguages
    {
        public void ApplySeed(EntityTypeBuilder<tblLanguages> builder)
        {
            //builder.HasData(new List<tblLanguages>() {
            //    new tblLanguages()
            //    {
            //        Id=new Guid().SequentialGuid(),
            //        Name="Persian",
            //        NativeName="فارسی",
            //        IsRtl=true,
            //        IsActive=true,
            //        Abbr="fa",
            //        Code="fa-IR",
            //        UseForSideLanguage=true,
            //    }
            //});
        }
    }
}
