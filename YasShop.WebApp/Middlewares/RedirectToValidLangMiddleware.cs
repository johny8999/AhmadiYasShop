using Framework.Const;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Languages;
using YasShop.Application.Languages;
using YasShop.WebApp.Common.Utilities.IpAddress;

namespace YasShop.WebApp.Middlewares
{
    public class RedirectToValidLangMiddleware
    {
        private readonly RequestDelegate _next;

        public RedirectToValidLangMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method.ToLower().Equals("get"))
            {
                string Path = context.Request.Path.Value ?? "";
                string[] Paths = Path.Trim('/').Split('/');

                if (Paths.Where(a => a != "" && a != null).Any())
                {
                    var LangAbbr = Paths[0];
                    //TODO : check for Language Dictionary
                    var _languageApplication = context.RequestServices.GetService<ILanguagesApplication>();
                    bool? IsValid = await _languageApplication.IsValidAbbrForSiteLangAsync(new InpIsValidAbbrForSiteLang() { Abbr=LangAbbr});
                    if (!IsValid.GetValueOrDefault(false))
                    {
                        Paths[0] = "fa";
                        context.Response.StatusCode = 301;
                        context.Response.Redirect(SiteSettingConst.SiteUrl + "/" + string.Join("/", Paths));
                        //context.Response.Redirect(context.Request.Scheme+"://"+context.Request.Host.Value+"/"+ string.Join("/",Paths));
                    }
                }
                else
                {

                    string SiteUrl = context.Request.Scheme + "://" + context.Request.Host.Value;
                        //SiteSettingConst.SiteUrl;
                    string LangAbbr = GetLangByIpAddress(context);

                    context.Response.Redirect(SiteUrl + "/" + LangAbbr);

                }
            }
            await _next(context);
        }

        private string GetLangByIpAddress(HttpContext context)
        {
            var _IpAddressChecker = context.RequestServices.GetService<IIpAddressChecker>();

            var _LangAbbr = _IpAddressChecker.GetLangAbbr(context.Connection.LocalIpAddress.ToString());
            if (_LangAbbr is null)
            {
                return "fa";
            }
            else
            {
                return _LangAbbr;
            }

        }
    }
}
