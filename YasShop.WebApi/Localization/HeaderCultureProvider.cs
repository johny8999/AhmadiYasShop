using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Linq;
using System.Threading.Tasks;
using YasShop.Application.Languages;


namespace YasShop.WebApi.Localization
{
    public class HeaderCultureProvider : RequestCultureProvider
    {
        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {

            if (httpContext == null)
                throw new ArgumentNullException();

            var _LanguageApplication = (ILanguagesApplication)httpContext.RequestServices.GetService(typeof(ILanguagesApplication));

            if (!httpContext.Request.Headers.Any(a => a.Key == "langCode"))
                return new ProviderCultureResult("fa-IR", "fa-IR");


            string langCode = httpContext.Request.Headers.Where(a => a.Key == "langCode").Select(a => a.Value).Single();
            string cultureName = langCode.Split("-")[0];

            var LangCode = await _LanguageApplication.GetCodeByAbbrAsync(cultureName);


            return new ProviderCultureResult(LangCode, LangCode);
        }

    }
}
