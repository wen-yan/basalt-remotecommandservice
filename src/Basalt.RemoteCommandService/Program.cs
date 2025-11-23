using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Yaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;

namespace Basalt.RemoteCommandService;

static class Program
{
    private static async Task<int> Main(string[] args)
    {
        bool useConsole = Debugger.IsAttached || args.Contains("--console");
        
        HostApplicationBuilder hostBuilder = Host.CreateApplicationBuilder(args);

        if (!useConsole)
        {
            hostBuilder.Services.AddWindowsService(options => { options.ServiceName = "Basalt.RemoteCommandService"; });
        }

        LoggerProviderOptions.RegisterProviderOptions<
            EventLogSettings, EventLogLoggerProvider>(hostBuilder.Services);

        hostBuilder.Services.AddHostedService<RemoteCommandWindowsService>();

        hostBuilder.Configuration
            .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new ApplicationException())
            .AddYamlFile("appsettings.yaml", false, false);

        var host = hostBuilder.Build();
        await host.RunAsync();

        return 0;
    }
}