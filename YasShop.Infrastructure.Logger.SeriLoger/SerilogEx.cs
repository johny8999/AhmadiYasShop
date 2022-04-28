using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
