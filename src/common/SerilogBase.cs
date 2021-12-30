using Serilog;
using Serilog.Events;
using System.Net;
using System.Runtime.InteropServices;

namespace common
{
    public static class SerilogBase
    {
        public static void SetLoggerConfiguration(string appName)
        {
            var isWindows = System.Runtime.InteropServices.RuntimeInformation
                                               .IsOSPlatform(OSPlatform.Windows);
            var hostName = Dns.GetHostName();
            var basePath = @"c:\logs";
            var path = basePath + $"/{appName}.log";
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)

                .Enrich.WithProperty("host", $"{appName}-{hostName}")
                .Enrich.FromLogContext()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Verbose, outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level}] {EventId} {Message:lj} {Properties},{Exception}{NewLine}")
                .WriteTo.File( path, retainedFileCountLimit: 12)
                

                .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(msg => Console.Write(msg));
        }
    }
}
