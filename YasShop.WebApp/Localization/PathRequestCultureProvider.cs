using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YasShop.Application.Languages;
using YasShop.WebApp.Common.Utilities.IpAddress;

namespace YasShop.WebApp.Localization
{
    public class PathRequestCultureProvider : RequestCultureProvider
    {
        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {

            if (httpContext == null)
                throw new ArgumentNullException();

            var _LanguageApplication = (ILanguagesApplication)httpContext.RequestServices
                                                                         .GetService(typeof(ILanguagesApplication));


            string Path = httpContext.Request.Path;
            string CultureName = Path.Trim().Trim('/').Split("/").First().ToLower();

            var LangCode = await _LanguageApplication.GetCodeByAbbrAsync(CultureName);
           
            if (LangCode == null)
            {
                var _IpAddressCheker = (IIpAddressChecker)httpContext.RequestServices.GetService(typeof(IIpAddressChecker));
                var _UserIpAddress = httpContext.Connection.RemoteIpAddress.ToString();

                if (_IpAddressCheker.CheckIp(_UserIpAddress)=="ir")
                {
                    LangCode = "fa-IR";
                }
                else if (_IpAddressCheker.CheckIp(_UserIpAddress)=="us")
                {
                    LangCode = "en-US";
                }
                else
                {
                    LangCode = "fa-IR";
                }
            }

            return new ProviderCultureResult(LangCode, LangCode);
        }

    }
}
