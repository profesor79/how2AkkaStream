using Akka.Configuration;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public static class AkkaConfiguration
    {
        public static string AkkaConfigurationString = @"
akka {
    log-config-on-start = on
    log-dead-letters = on
    loglevel = DEBUG,
    loggers =[""Akka.Logger.Serilog.SerilogLogger, Akka.Logger.Serilog""]}
    actor {
        throughput = 1
        provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
        }
remote {
        dot-netty.tcp {
            port = 0
            hostname = localhost
            public-hostname = __publicHostName__
              # Sets the send buffer size of the Sockets,
# set to 0b for platform defaultb0
send-buffer-size = 33554432b
              # Sets the receive buffer size of the Sockets,
# set to 0b for platform default
receive-buffer-size = 33554432b
              # Maximum message size the transport will accept, but at least
# 32000 bytes.
# Please note that UDP does not support arbitrary large datagrams,
# so this setting has to be chosen carefully when using UDP.
# Both send-buffer-size and receive-buffer-size settings has to
# be adjusted to be able to buffer messages of maximum size.
maximum-frame-size = 16777216b
        }
    }
}
                    ";

        public static Config GetConfig(string systemName)
        {
            var hostname = Dns.GetHostName();
            Log.Information("hostname", hostname);

            var config = ConfigurationFactory.ParseString(
                AkkaConfigurationString.Replace("__publicHostName__", systemName)
              );

            return config;
        }
    }

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
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug, outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level}] {EventId} {Message:lj} {Properties},{Exception}{NewLine}")
                .WriteTo.File( path, retainedFileCountLimit: 12)
                

                .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(msg => Console.Write(msg));
        }
    }
}
