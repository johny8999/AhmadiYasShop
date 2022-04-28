using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using YasShop.Domain.Region.LanguageAgg.Contract;
using YasShop.Domain.Region.LanguageAgg.Entities;

namespace YasShop.Infrastructure.Seed.Base.Languages
{
    public class Seed_Language : ISeed_Language
    {
        private readonly ILanguagesRepository _LanguagesRepository;
        private readonly ILogger _Logger;

        public Seed_Language(ILanguagesRepository languagesRepository, ILogger logger)
        {
            _LanguagesRepository = languagesRepository;
            _Logger = logger;
        }

        public async Task<bool> RunAsync()
        {
            try
            {
                if (!await _LanguagesRepository.Get.AnyAsync(a => a.Name.Equals("Persian(IR)")))
                {
                    await _LanguagesRepository.AddAsync(new tblLanguages
                    {
                        Id = new Guid().SequentialGuid(),
                        Name = "Persian(IR)",
                        NativeName = "فارسی(ایران)",
                        Code = "fa-IR",
                        Abbr = "fa",
                        IsActive = true,
                        IsRtl = true,
                        UseForSiteLanguage = true
                    });

                }

                if (!await _LanguagesRepository.Get.AnyAsync(a => a.Name.Equals("English(US)")))
                {
                    await _LanguagesRepository.AddAsync(new tblLanguages
                    {
                        Id = new Guid().SequentialGuid(),
                        Name = "English(US)",
                        NativeName = "English(USA)",
                        Code = "en-US",
                        Abbr = "en",
                        IsActive = true,
                        IsRtl = true,
                        UseForSiteLanguage = true
                    });

                }

                return true;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }
    }
}
