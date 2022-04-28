using System.Collections.Generic;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Languages;
using YasShop.Application.Contracts.ApplicationDTO.Result;

namespace YasShop.Application.Languages
{
    public interface ILanguagesApplication
    {
        Task<OperationResult> AddLanguageAsync(InpAddLanguage Input);
        Task<bool> CheckExistAsync(string Name = null, string NativeName = null, string Code = null, string Abbr = null);
        Task<List<OutSiteLanguageCache>> GetAllLanguageSiteLangAsync();
        Task<string> GetCodeByAbbrAsync(string Abbr);
        Task<bool?> IsValidAbbrForSiteLangAsync(InpIsValidAbbrForSiteLang input);
    }
}