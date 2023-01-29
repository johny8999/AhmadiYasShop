using Microsoft.AspNetCore.Builder;
using Serilog;

namespace YasShop.Infrastructure.Logger.SeriLoger
{
    public static class SerilogEx
    {
        public static void UseSerilog_SqlServer(this ConfigureHostBuilder webHostBuilder)
        {
            webHostBuilder.UseSerilog((builder, Logger) =>
            {
                Logger = new SeriLogConfig().ConfigSqlServer(Serilog.Events.LogEventLevel.Warning);
                Logger.CreateLogger();
            });
        }
        public static void UseSerilog_Console(this ConfigureHostBuilder webHostBuilder)
        {
            webHostBuilder.UseSerilog((builder, Logger) =>
            {
                Logger.WriteTo.Console().MinimumLevel.Is(Serilog.Events.LogEventLevel.Information);
            });
        }

    }
}
