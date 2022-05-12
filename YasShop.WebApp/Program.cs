using Framework.Const;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using YasShop.Application.Contracts.Mappings;
using YasShop.Infrastructure.Core.Configuration;
using YasShop.Infrastructure.Logger.SeriLoger;
using YasShop.Infrastructure.Seed.Base.Main;
using YasShop.WebApp.Authentication;
using YasShop.WebApp.Config;
using YasShop.WebApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);
WebApplication app = null;

#region ConfigureServices
{

    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
    {
        builder.Host.UseSerilog_Console();
    }
    else
    {
        builder.Host.UseSerilog_SqlServer();
    }

    builder.Services.AddAntiforgery(a => a.HeaderName = "XSRF-TOKEN");

    builder.Services.AddCustomLocalization();

    builder.Services.AddRazorPage()
            .AddCustomViewLocalization()
            .AddCustomDataAnnotationLocalization(builder.Services);

    builder.Services.Config();
    builder.Services.AddInject();


    builder.Services.AddCustomIdentity();
    builder.Services.AddJwtAuthentication();
    //builder.Services.AddAutoMapper(typeof(UserProfile));
}
#endregion ConfigureServices

#region Configure
{
    app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    //app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseCustomLocalization();
    app.UseJWTAuthentication(AuthConst.SecretKey,AuthConst.CookieName);

    app.UseMiddleware<RedirectToValidLangMiddleware>();
    app.UseEndpoints(endpoints =>
    {

        endpoints.MapRazorPages();
    });
}
#endregion Configure

#region ConfigureSeed
{

    using (var ServiceScope = app.Services.CreateScope())
    {
        var Services = ServiceScope.ServiceProvider;
        try
        {
            var _SeedMain = Services.GetRequiredService<ISeed_main>();

            _SeedMain.RunAsync().Wait();
            //var q= _SeedMain.RunAsync().Result;   //for return result
        }
        catch (Exception ex)
        {
            var _Logger = Services.GetRequiredService<ILogger>();
            _Logger.Fatal(ex);
        }
    }
}
#endregion ConfigureSeed
app.Run();