using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Commands;
using SwiftlyS2.Core.ConsoleOutput;
using SwiftlyS2.Core.Events;
using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Core.Misc;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.ConsoleOutput;
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
using SwiftlyS2.Core.Database;
using SwiftlyS2.Shared.Database;
using SwiftlyS2.Core.Translations;
using SwiftlyS2.Core.Permissions;
using SwiftlyS2.Shared.Permissions;
using SwiftlyS2.Core.Menus;
using SwiftlyS2.Shared.Menus;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.Translation;
using SwiftlyS2.Core.Players;
using SwiftlyS2.Shared.CommandLine;
using SwiftlyS2.Core.CommandLine;
using SwiftlyS2.Shared.Helpers;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.FileSystem;
using SwiftlyS2.Shared.FileSystem;

namespace SwiftlyS2.Core.Services;

internal class SwiftlyCore : ISwiftlyCore, IDisposable
{
    private readonly ServiceProvider serviceProvider;

    public EventSubscriber EventSubscriber { get; init; }
    public GameEventService GameEventService { get; init; }
    public NetMessageService NetMessageService { get; init; }
    public PluginConfigurationService Configuration { get; init; }
    public ILoggerFactory LoggerFactory { get; init; }
    public CommandService CommandService { get; init; }
    public ConsoleOutputService ConsoleOutputService { get; init; }
    public EntitySystemService EntitySystemService { get; init; }
    public ConVarService ConVarService { get; init; }
    public GameDataService GameDataService { get; init; }
    public PlayerManagerService PlayerManagerService { get; init; }
    public ILogger Logger { get; init; }
    public EngineService Engine { get; init; }
    public TraceManager Trace { get; init; }
    public ContextedProfilerService ProfilerService { get; init; }
    public MemoryService MemoryService { get; init; }
    public SchedulerService SchedulerService { get; init; }
    public DatabaseService DatabaseService { get; init; }
    public TranslationService TranslationService { get; init; }
    public Localizer Localizer { get; init; }
    public PermissionManager PermissionManager { get; init; }
    public RegistratorService RegistratorService { get; init; }
    // [Obsolete("MenuManager will be deprecared at the release of SwiftlyS2. Please use MenuManagerAPI instead")]
    // public MenuManager MenuManager { get; init; }
    public MenuManagerAPI MenuManagerAPI { get; init; }
    public CommandLineService CommandLineService { get; init; }
    public HelpersService Helpers { get; init; }
    public string ContextBasePath { get; init; }
    public string PluginDataDirectory { get; init; }
    public GameFileSystem GameFileSystem { get; init; }
    public SwiftlyCore( string contextId, string contextBaseDirectory, PluginMetadata? pluginManifest, Type contextType, IServiceProvider coreProvider, string pluginDataDirectory )
    {

        CoreContext id = new(contextId, contextBaseDirectory, pluginManifest);
        ContextBasePath = contextBaseDirectory;
        PluginDataDirectory = pluginDataDirectory;

        serviceProvider = new ServiceCollection()
            .AddSingleton(id)
            .AddSingleton(this)
            .AddSingleton<ISwiftlyCore>(this)
            .AddSingleton(coreProvider.GetRequiredService<ProfileService>())
            .AddSingleton(coreProvider.GetRequiredService<ConfigurationService>())
            .AddSingleton(coreProvider.GetRequiredService<HookManager>())
            .AddSingleton(coreProvider.GetRequiredService<PlayerManagerService>())
            .AddSingleton(coreProvider.GetRequiredService<TraceManager>())
            .AddSingleton(coreProvider.GetRequiredService<PermissionManager>())
            .AddSingleton(coreProvider.GetRequiredService<CommandTrackerManager>())

            .AddSingleton<EventSubscriber>()
            .AddSingleton<EngineService>()
            .AddSingleton<PluginConfigurationService>()
            .AddSingleton<GameEventService>()
            .AddSingleton<NetMessageService>()
            .AddSingleton<CommandService>()
            .AddSingleton<ConsoleOutputService>()
            .AddSingleton<EntitySystemService>()
            .AddSingleton<ConVarService>()
            .AddSingleton<MemoryService>()
            .AddSingleton<GameDataService>()
            .AddSingleton<PlayerManagerService>()
            .AddSingleton<ContextedProfilerService>()
            .AddSingleton<GameFileSystem>()
            .AddSingleton<SchedulerService>()
            .AddSingleton<DatabaseService>()
            .AddSingleton<TranslationService>()
            .AddSingleton<Localizer>(provider => provider.GetRequiredService<TranslationService>().GetLocalizer())
            .AddSingleton<RegistratorService>()
            // .AddSingleton<MenuManager>()
            .AddSingleton<MenuManagerAPI>()
            .AddSingleton<CommandLineService>()
            .AddSingleton<HelpersService>()
            .AddSingleton<IPermissionManager>(provider => provider.GetRequiredService<PermissionManager>())

            .AddSingleton<IEventSubscriber>(provider => provider.GetRequiredService<EventSubscriber>())
            .AddSingleton<IGameEventService>(provider => provider.GetRequiredService<GameEventService>())
            .AddSingleton<INetMessageService>(provider => provider.GetRequiredService<NetMessageService>())
            .AddSingleton<IPluginConfigurationService>(provider => provider.GetRequiredService<PluginConfigurationService>())
            .AddSingleton<ICommandService>(provider => provider.GetRequiredService<CommandService>())
            .AddSingleton<IConsoleOutputService>(provider => provider.GetRequiredService<ConsoleOutputService>())
            .AddSingleton<IEntitySystemService>(provider => provider.GetRequiredService<EntitySystemService>())
            .AddSingleton<IConVarService>(provider => provider.GetRequiredService<ConVarService>())
            .AddSingleton<IGameDataService>(provider => provider.GetRequiredService<GameDataService>())
            .AddSingleton<IPlayerManagerService>(provider => provider.GetRequiredService<PlayerManagerService>())
            .AddSingleton<IMemoryService>(provider => provider.GetRequiredService<MemoryService>())
            .AddSingleton<IContextedProfilerService>(provider => provider.GetRequiredService<ContextedProfilerService>())
            .AddSingleton<ISchedulerService>(provider => provider.GetRequiredService<SchedulerService>())
            .AddSingleton<IEngineService>(provider => provider.GetRequiredService<EngineService>())
            .AddSingleton<ITraceManager>(provider => provider.GetRequiredService<TraceManager>())
            .AddSingleton<IDatabaseService>(provider => provider.GetRequiredService<DatabaseService>())
            .AddSingleton<ITranslationService>(provider => provider.GetRequiredService<TranslationService>())
            .AddSingleton<ILocalizer>(provider => provider.GetRequiredService<TranslationService>().GetLocalizer())
            .AddSingleton<IRegistratorService>(provider => provider.GetRequiredService<RegistratorService>())
            // .AddSingleton<IMenuManager>(provider => provider.GetRequiredService<MenuManager>())
            .AddSingleton<IMenuManagerAPI>(provider => provider.GetRequiredService<MenuManagerAPI>())
            .AddSingleton<ICommandLine>(provider => provider.GetRequiredService<CommandLineService>())
            .AddSingleton<IHelpers>(provider => provider.GetRequiredService<HelpersService>())
            .AddSingleton<IGameFileSystem>(provider => provider.GetRequiredService<GameFileSystem>())

            .AddLogging(builder => builder.AddProvider(new SwiftlyLoggerProvider(id.Name)))

            .BuildServiceProvider();

        EventSubscriber = serviceProvider.GetRequiredService<EventSubscriber>();
        Configuration = serviceProvider.GetRequiredService<PluginConfigurationService>();
        GameEventService = serviceProvider.GetRequiredService<GameEventService>();
        LoggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        NetMessageService = serviceProvider.GetRequiredService<NetMessageService>();
        CommandService = serviceProvider.GetRequiredService<CommandService>();
        ConsoleOutputService = serviceProvider.GetRequiredService<ConsoleOutputService>();
        EntitySystemService = serviceProvider.GetRequiredService<EntitySystemService>();
        GameDataService = serviceProvider.GetRequiredService<GameDataService>();
        PlayerManagerService = serviceProvider.GetRequiredService<PlayerManagerService>();
        ConVarService = serviceProvider.GetRequiredService<ConVarService>();
        MemoryService = serviceProvider.GetRequiredService<MemoryService>();
        Engine = serviceProvider.GetRequiredService<EngineService>();
        Trace = serviceProvider.GetRequiredService<TraceManager>();
        ProfilerService = serviceProvider.GetRequiredService<ContextedProfilerService>();
        SchedulerService = serviceProvider.GetRequiredService<SchedulerService>();
        DatabaseService = serviceProvider.GetRequiredService<DatabaseService>();
        TranslationService = serviceProvider.GetRequiredService<TranslationService>();
        Localizer = serviceProvider.GetRequiredService<Localizer>();
        PermissionManager = serviceProvider.GetRequiredService<PermissionManager>();
        RegistratorService = serviceProvider.GetRequiredService<RegistratorService>();
        // MenuManager = serviceProvider.GetRequiredService<MenuManager>();
        MenuManagerAPI = serviceProvider.GetRequiredService<MenuManagerAPI>();
        CommandLineService = serviceProvider.GetRequiredService<CommandLineService>();
        Helpers = serviceProvider.GetRequiredService<HelpersService>();
        Logger = LoggerFactory.CreateLogger(contextType);
        GameFileSystem = serviceProvider.GetRequiredService<GameFileSystem>();
    }

    public void InitializeType( Type type )
    {
        this.Parse(type);
    }

    public void InitializeObject( object instance )
    {
        RegistratorService.Register(instance);
    }

    public void Dispose()
    {
        serviceProvider.Dispose();
    }

    IEventSubscriber ISwiftlyCore.Event => EventSubscriber;
    IPluginConfigurationService ISwiftlyCore.Configuration => Configuration;
    ILoggerFactory ISwiftlyCore.LoggerFactory => LoggerFactory;
    IGameEventService ISwiftlyCore.GameEvent => GameEventService;
    INetMessageService ISwiftlyCore.NetMessage => NetMessageService;
    ICommandService ISwiftlyCore.Command => CommandService;
    IConsoleOutputService ISwiftlyCore.ConsoleOutput => ConsoleOutputService;
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
    IDatabaseService ISwiftlyCore.Database => DatabaseService;
    ITranslationService ISwiftlyCore.Translation => TranslationService;
    ILocalizer ISwiftlyCore.Localizer => Localizer;
    IPermissionManager ISwiftlyCore.Permission => PermissionManager;
    IRegistratorService ISwiftlyCore.Registrator => RegistratorService;
    // [Obsolete("MenuManager will be deprecared at the release of SwiftlyS2. Please use MenuManagerAPI instead")]
    // IMenuManager ISwiftlyCore.Menus => MenuManager;
    IMenuManagerAPI ISwiftlyCore.MenusAPI => MenuManagerAPI;
    string ISwiftlyCore.PluginPath => ContextBasePath;
    string ISwiftlyCore.CSGODirectory => NativeEngineHelpers.GetCSGODirectoryPath();
    string ISwiftlyCore.GameDirectory => NativeEngineHelpers.GetGameDirectoryPath();
    ICommandLine ISwiftlyCore.CommandLine => CommandLineService;
    IHelpers ISwiftlyCore.Helpers => Helpers;
    IGameFileSystem ISwiftlyCore.GameFileSystem => GameFileSystem;
}