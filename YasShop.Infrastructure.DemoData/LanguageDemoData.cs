using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YasShop.Application.Contracts.Languages;
using YasShop.Application.Languages;

namespace YasShop.Infrastructure.DemoData
{
    public class LanguageDemoData
    {
        private readonly ILanguagesApplication _LanguagesApplication;

        public LanguageDemoData(ILanguagesApplication languagesApplication)
        {
            _LanguagesApplication = languagesApplication;
        }

        public async Task<bool> RunAsync()
        {
            if (await _LanguagesApplication.CheckExistAsync("Persian(IR)"))
            {
                await _LanguagesApplication.AddLanguageAsync(new InpAddLanguage
                {
                    Name = "Persian(IR)",
                    NativeName = "فارسی(ایران)",
                    Code = "fa-IR",
                    Abbr = "fa",
                    IsActive = true,
                    IsRtl = true,
                    UseForSideLanguage = true
                });

            }

            if (await _LanguagesApplication.CheckExistAsync("English(US)"))
            {
                await _LanguagesApplication.AddLanguageAsync(new InpAddLanguage
                {
                    Name = "English(US)",
                    NativeName = "English(USA)",
                    Code = "en-US",
                    Abbr = "US",
                    IsActive = true,
                    IsRtl = true,
                    UseForSideLanguage = true
                });

            }
        }
    }
}
