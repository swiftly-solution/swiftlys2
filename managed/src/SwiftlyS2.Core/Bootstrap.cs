using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using SwiftlyS2.Core.Events;
using SwiftlyS2.Core.Hosting;
using SwiftlyS2.Core.Misc;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.SteamAPI;
namespace SwiftlyS2.Core;

internal static class Bootstrap
{

    // how tf i forgot services can be collected hahahahahahahhaahhahaa FUCK
    private static IHost? _host;

    private static IntPtr SteamAPIDLLResolver( string libraryName, Assembly assembly, DllImportSearchPath? searchPath )
    {
        if (libraryName == "steam_api" || libraryName == "sdkencryptedappticket")
        {
            if (OperatingSystem.IsWindows())
            {
                libraryName += "64";
            }

            if (NativeLibrary.TryLoad(libraryName, out var handle))
            {
                return handle;
            }
        }

        return IntPtr.Zero;
    }

    public static void Start( IntPtr nativeTable, int nativeTableSize, string basePath )
    {
        Environment.SetEnvironmentVariable("SWIFTLY_MANAGED_ROOT", basePath);

        NativeBinding.BindNatives(nativeTable, nativeTableSize);

        NativeLibrary.SetDllImportResolver(typeof(NativeMethods).Assembly, SteamAPIDLLResolver);

        EventPublisher.Register();
        GameFunctions.Initialize();
        FileLogger.Initialize(basePath);

        AnsiConsole.Write(new FigletText("SwiftlyS2").LeftJustified().Color(Color.LightSteelBlue1));

        _host = Host.CreateDefaultBuilder()
          .UseConsoleLifetime(options =>
          {
              options.SuppressStatusMessages = true;
          })
          .ConfigureServices(( context, services ) =>
          {
              _ = services.AddHostedService<StartupService>();
          })
          .ConfigureLogging(( context, logging ) =>
          {
              _ = logging.ClearProviders();
              _ = logging.AddProvider(new SwiftlyLoggerProvider("SwiftlyS2"));
          })
          .ConfigureAppConfiguration(( context, config ) =>
          {
              _ = config.SetBasePath(Path.Combine(Environment.GetEnvironmentVariable("SWIFTLY_MANAGED_ROOT")!, "configs"));
              _ = config.AddJsonFile("permissions.jsonc", optional: false, reloadOnChange: true);
          })
          .ConfigureServices(( context, services ) =>
          {
              _ = services
            .AddProfileService()
            .AddConfigurationService()
            .AddTestService()
            .AddRootDirService()
            .AddDataDirectoryService()
            .AddPlayerManagerService()
            .AddPluginManager()
            .AddHookManager()
            .AddTraceManagerService()
            .AddPermissionManager()
            .AddCoreHookService()
            .AddCoreCommandService()
            .AddCommandTrackerManager()
            .AddCommandTrackerService()
            .AddSwiftlyCore(basePath);
          })
          .Build();

        _host.Start();

        // provider.UseTestService();

    }
}