using Framework.Application.Services.Localizer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;
using YasShop.WebApp.Common.Utilities.IpAddress;
using YasShop.WebApp.Common.Utilities.MessageBox;
using YasShop.WebApp.Localization;
using YasShop.WebApp.Localization.Resources;

namespace YasShop.WebApp.Config
{
    public static class StartupEx
    {
        public static IMvcBuilder AddRazorPage(this IServiceCollection services)
        {
            return services.AddRazorPages(opt =>
            {
                opt.Conventions.AddPageRoute("/Home/Index", "");
            });

        }

        public static IServiceCollection AddCustomLocalization(this IServiceCollection services)
        {
            return services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "Localization/Resources";
            });
        }

        public static IMvcBuilder AddCustomViewLocalization(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, opt =>
            {
                opt.ResourcesPath = "Localization/Resources";
            });
        }

        public static IMvcBuilder AddCustomDataAnnotationLocalization(this IMvcBuilder builder, IServiceCollection services)
        {
            return builder.AddDataAnnotationsLocalization(opt =>
            {
                var Localizer = new FactoryLocalizer().Set(services, typeof(SharedResources));
                opt.DataAnnotationLocalizerProvider = (t, f) => Localizer;
            });
        }

        public static IApplicationBuilder UseCustomLocalization(this IApplicationBuilder app)
        {
            var SupportedCulture = new List<CultureInfo>()
            {
                 new CultureInfo("fa-IR"),
                 new CultureInfo("en-US")
            };
            var Options = new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture("fa-IR"),
                SupportedCultures = SupportedCulture,
                SupportedUICultures = SupportedCulture,
                RequestCultureProviders = new List<IRequestCultureProvider>()
                {
                   new PathRequestCultureProvider()

                    #region 
                    //new CookieRequestCultureProvider(){ 
                    //CookieName="test"
                    //},
                    //new QueryStringRequestCultureProvider()
                    //{
                    //    //QueryStringKey,

                    //}
                    #endregion

                },
            };
            return app.UseRequestLocalization(Options);
        }

        public static IServiceCollection AddInject(this IServiceCollection services)
        {
            services.AddSingleton<ILocalizer, Localizer>();
            services.AddScoped<IIpAddressChecker, IpAddressChecker>();
            services.AddScoped<IMsgBox,MsgBox>();
            return services;
        }


    }
}
