using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;

namespace YasShop.Infrastructure.Logger.SeriLoger
{
    public class SeriLogConfig
    {
        public LoggerConfiguration ConfigSqlServer(LogEventLevel logEventLevel)
        {
            var ColumnOpt = new ColumnOptions();
            ColumnOpt.Store.Remove(StandardColumn.Properties);
            ColumnOpt.Store.Add(StandardColumn.LogEvent);
            ColumnOpt.LogEvent.DataLength = -1;//-1 => max
            ColumnOpt.PrimaryKey = ColumnOpt.TimeStamp;
            ColumnOpt.TimeStamp.NonClusteredIndex = true;

            return new LoggerConfiguration()
                                       .Enrich.FromLogContext()
                                       .MinimumLevel.Is(logEventLevel)
                                       .WriteTo.MSSqlServer("Server=.;Database=LogsDb;Trusted_Connection=True;", new MSSqlServerSinkOptions()
                                       {
                                           AutoCreateSqlTable = true,
                                           TableName = "tblYasShoplogs",
                                           BatchPeriod = new TimeSpan(0, 0, 1)
                                       }, columnOptions: ColumnOpt);
        }
    }
}
