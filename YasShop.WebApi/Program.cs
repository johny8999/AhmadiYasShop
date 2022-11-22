using Framework.Const;
using YasShop.Infrastructure.Core.Configuration;
using YasShop.Infrastructure.Logger.SeriLoger;
using YasShop.Infrastructure.Seed.Base.Main;
using YasShop.WebApp.Authentication;
using YasShop.WebApp.Config;
using ILogger = Framework.Infrastructure.ILogger;

var builder = WebApplication.CreateBuilder(args);

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
{
    builder.Host.UseSerilog_Console();
}
else
{
    builder.Host.UseSerilog_SqlServer();
}
builder.Services.AddCustomLocalization();
builder.Services.AddControllers().AddCustomViewLocalization("Localization/Resource")
    .AddCustomDataAnnotationLocalization(builder.Services)
    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver =
        new Newtonsoft.Json.Serialization.DefaultContractResolver());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Config();
builder.Services.AddInject();

builder.Services.AddCustomIdentity();

builder.Services.AddJwtAuthentication();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCustomLocalization();
app.UseJwtAuthentication(AuthConst.SecretKey, AuthConst.CookieName);
app.UseAuthorization();

app.MapControllers();

#region ConfigureSeed

{
    using (var ServiceScope = app.Services.CreateScope())
    {
        var Services = ServiceScope.ServiceProvider;
        try
        {
            //TODO:Convert to page
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