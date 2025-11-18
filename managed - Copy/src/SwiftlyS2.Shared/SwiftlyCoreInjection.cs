using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared.Commands;

namespace SwiftlyS2.Shared;

public static class SwiftlyCoreInjection {

  public static IServiceCollection AddSwiftly(this IServiceCollection self, ISwiftlyCore core, bool addLogger = true, bool addConfiguration = true)
  {
    self
      .AddSingleton(core)
      .AddSingleton(core.ConVar)
      .AddSingleton(core.Command)
      .AddSingleton(core.Database)
      .AddSingleton(core.Engine)
      .AddSingleton(core.EntitySystem)
      .AddSingleton(core.Event)
      .AddSingleton(core.GameData)
      .AddSingleton(core.GameEvent)
      .AddSingleton(core.Localizer)
      .AddSingleton(core.Memory)
      .AddSingleton(core.NetMessage)
      .AddSingleton(core.Permission)
      .AddSingleton(core.PlayerManager)
      .AddSingleton(core.Profiler)
      .AddSingleton(core.Scheduler)
      .AddSingleton(core.Trace)
      .AddSingleton(core.Translation);

    if (addLogger) {
      self
        .AddSingleton(core.LoggerFactory)
        .AddSingleton(typeof(ILogger<>), typeof(Logger<>));
    }

    if (addConfiguration && core.Configuration.BasePathExists) {
      self
        .AddSingleton(core.Configuration)
        .AddSingleton(core.Configuration.Manager)
        .AddSingleton<IConfiguration>(provider => provider.GetRequiredService<IConfigurationManager>());
    }

    return self;
  }

}