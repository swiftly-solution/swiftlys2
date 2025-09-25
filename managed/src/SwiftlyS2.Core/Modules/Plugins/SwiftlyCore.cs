using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Commands;
using SwiftlyS2.Core.Events;
using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Core.Misc;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.Services;
using SwiftlyS2.Core.AttributeParsers;
using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Shared.EntitySystem;
using SwiftlyS2.Core.Convars;
using SwiftlyS2.Shared.Convars;
using SwiftlyS2.Core.Hooks;
using SwiftlyS2.Shared.Profiler;
using SwiftlyS2.Core.Profiler;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Core.Memory;
using SwiftlyS2.Shared.Scheduler;
using SwiftlyS2.Core.Scheduler;

namespace SwiftlyS2.Core.Services;


internal class SwiftlyCore : ISwiftlyCore, IDisposable {

  private ServiceProvider _ServiceProvider { get; init; }

  public EventSubscriber EventSubscriber { get; init; }
  public GameEventService GameEventService { get; init; }
  public NetMessageService NetMessageService { get; init; }
  public PluginConfigurationService Configuration { get; init; }
  public ILoggerFactory LoggerFactory { get; init; }
  public CommandService CommandService { get; init; }
  public IEntitySystemService EntitySystemService { get; init; }
  public IConVarService ConVarService { get; init; }
  public IGameDataService GameDataService { get; init; }
  public IPlayerManagerService PlayerManagerService { get; init; }
  public ILogger Logger { get; init; }
  public IEngineService Engine { get; init; }
  public ITraceManager Trace { get; init; }
  public IContextedProfilerService ProfilerService { get; init; }
  public IMemoryService MemoryService { get; init; }
  public ISchedulerService SchedulerService { get; init; }

  public SwiftlyCore(string contextId, string contextBaseDirectory, Type contextType, IServiceProvider coreProvider) {

    CoreContext id = new(contextId, contextBaseDirectory);

    ServiceCollection services = new();

    services
      .AddSingleton(id)
      .AddSingleton(this)
      .AddSingleton(coreProvider.GetRequiredService<ProfileService>())
      .AddSingleton(coreProvider.GetRequiredService<ConfigurationService>())
      .AddSingleton(coreProvider.GetRequiredService<HookManager>())
      .AddSingleton(coreProvider.GetRequiredService<PlayerManagerService>())
      .AddSingleton(coreProvider.GetRequiredService<EngineService>())
      .AddSingleton(coreProvider.GetRequiredService<TraceManager>())

      .AddSingleton<IEventSubscriber, EventSubscriber>()
      .AddSingleton<PluginConfigurationService>()
      .AddSingleton<GameEventService>()
      .AddSingleton<NetMessageService>()
      .AddSingleton<CommandService>()
      .AddSingleton<EntitySystemService>()
      .AddSingleton<ConVarService>()
      .AddSingleton<MemoryService>()
      .AddSingleton<GameDataService>()
      .AddSingleton<IContextedProfilerService, ContextedProfilerService>()
      .AddSingleton<SchedulerService>()

      .AddLogging(
        builder => {
          builder.AddProvider(new SwiftlyLoggerProvider(id.Name));
        }
      );


    _ServiceProvider = services.BuildServiceProvider();

    EventSubscriber = _ServiceProvider.GetRequiredService<EventSubscriber>();
    Configuration = _ServiceProvider.GetRequiredService<PluginConfigurationService>();
    GameEventService = _ServiceProvider.GetRequiredService<GameEventService>();
    LoggerFactory = _ServiceProvider.GetRequiredService<ILoggerFactory>();
    NetMessageService = _ServiceProvider.GetRequiredService<NetMessageService>();
    CommandService = _ServiceProvider.GetRequiredService<CommandService>();
    EntitySystemService = _ServiceProvider.GetRequiredService<EntitySystemService>();
    GameDataService = _ServiceProvider.GetRequiredService<GameDataService>();
    PlayerManagerService = _ServiceProvider.GetRequiredService<PlayerManagerService>();
    ConVarService = _ServiceProvider.GetRequiredService<ConVarService>();
    MemoryService = _ServiceProvider.GetRequiredService<MemoryService>();
    Engine = _ServiceProvider.GetRequiredService<EngineService>();
    Trace = _ServiceProvider.GetRequiredService<TraceManager>();
    ProfilerService = _ServiceProvider.GetRequiredService<IContextedProfilerService>();
    SchedulerService = _ServiceProvider.GetRequiredService<SchedulerService>();

    Logger = LoggerFactory.CreateLogger(contextType);
  }

  public void InitializeType(Type type) {
    this.Parse(type);
  }

  public void InitializeObject(object instance)
  {
    CommandService.ParseFromObject(instance);
    GameEventService.ParseFromObject(instance);
    NetMessageService.ParseFromObject(instance);
  }

  public void Dispose() {
    _ServiceProvider.Dispose();
  }

  IEventSubscriber ISwiftlyCore.Event => EventSubscriber;
  IPluginConfigurationService ISwiftlyCore.Configuration => Configuration;
  ILoggerFactory ISwiftlyCore.LoggerFactory => LoggerFactory;
  IGameEventService ISwiftlyCore.GameEvent => GameEventService;
  INetMessageService ISwiftlyCore.NetMessage => NetMessageService;
  ICommandService ISwiftlyCore.Command => CommandService;
  IEntitySystemService ISwiftlyCore.EntitySystem => EntitySystemService;
  IConVarService ISwiftlyCore.ConVar => ConVarService;
  IGameDataService ISwiftlyCore.GameData => GameDataService;
  IPlayerManagerService ISwiftlyCore.PlayerManager => PlayerManagerService;
  IMemoryService ISwiftlyCore.Memory => MemoryService;
  ILogger ISwiftlyCore.Logger => Logger;
  IContextedProfilerService ISwiftlyCore.Profiler => ProfilerService;
  IEngineService ISwiftlyCore.Engine => Engine;
  ITraceManager ISwiftlyCore.Trace => Trace;
  ISchedulerService ISwiftlyCore.Scheduler => SchedulerService;
}