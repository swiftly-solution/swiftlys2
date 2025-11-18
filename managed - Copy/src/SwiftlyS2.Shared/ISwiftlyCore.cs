using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.CommandLine;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.ConsoleOutput;
using SwiftlyS2.Shared.Convars;
using SwiftlyS2.Shared.Database;
using SwiftlyS2.Shared.EntitySystem;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.FileSystem;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Helpers;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Menus;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.Permissions;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.Profiler;
using SwiftlyS2.Shared.Scheduler;
using SwiftlyS2.Shared.Services;
using SwiftlyS2.Shared.Translation;

namespace SwiftlyS2.Shared;

/// <summary>
/// Core interface of SwiftlyS2 framework.
/// </summary>
public interface ISwiftlyCore
{
    /// <summary>
    /// Custom event subscriber.
    /// </summary>
    public IEventSubscriber Event { get; }

    /// <summary>
    /// Gets the engine service used to perform core engine operations.
    /// </summary>
    public IEngineService Engine { get; }

    /// <summary>
    /// Game event service.
    /// </summary>
    public IGameEventService GameEvent { get; }

    /// <summary>
    /// Net message service.
    /// </summary>
    public INetMessageService NetMessage { get; }

    /// <summary>
    /// Helpers service.
    /// </summary>
    public IHelpers Helpers { get; }

    /// <summary>
    /// Command service.
    /// </summary>
    public ICommandService Command { get; }

    /// <summary>
    /// Console output service.
    /// </summary>
    public IConsoleOutputService ConsoleOutput { get; }

    /// <summary>
    /// Entity system service.
    /// </summary>
    public IEntitySystemService EntitySystem { get; }

    /// <summary>
    /// Convar service.
    /// </summary>
    public IConVarService ConVar { get; }

    /// <summary>
    /// Configuration service.
    /// </summary>
    public IPluginConfigurationService Configuration { get; }

    /// <summary>
    /// Game data service.
    /// </summary>
    public IGameDataService GameData { get; }

    /// <summary>
    /// Player manager service.
    /// </summary>
    public IPlayerManagerService PlayerManager { get; }


    /// <summary>
    /// Memory service.
    /// </summary>
    public IMemoryService Memory { get; }

    /// <summary>
    /// Logger factory.
    /// </summary>
    public ILoggerFactory LoggerFactory { get; }

    /// <summary>
    /// Default logger.
    /// </summary>
    public ILogger Logger { get; }

    /// <summary>
    /// Profiler service.
    /// </summary>
    public IContextedProfilerService Profiler { get; }

    /// <summary>
    /// Gets the trace manager used to control and configure tracing operations within the game.
    /// </summary>
    public ITraceManager Trace { get; }

    /// <summary>
    /// Scheduler service.
    /// </summary>
    public ISchedulerService Scheduler { get; }

    /// <summary>
    /// Database service.
    /// </summary>
    public IDatabaseService Database { get; }

    /// <summary>
    /// Translation service.
    /// </summary>
    public ITranslationService Translation { get; }

    /// <summary>
    /// Localizer.
    /// </summary>
    public ILocalizer Localizer { get; }

    /// <summary>
    /// Permission manager.
    /// </summary>
    public IPermissionManager Permission { get; }

    /// <summary>
    /// Registrator service.
    /// </summary>
    public IRegistratorService Registrator { get; }

    // /// <summary>
    // /// Menu manager.
    // /// </summary>
    // [Obsolete("IMenuManager will be deprecared at the release of SwiftlyS2. Please use IMenuManagerAPI instead")]
    // public IMenuManager Menus { get; }

    /// <summary>
    /// Menu manager API.
    /// </summary>
    public IMenuManagerAPI MenusAPI { get; }

    /// <summary>
    /// Command line.
    /// </summary>
    public ICommandLine CommandLine { get; }

    /// <summary>
    /// Gets the file path to the plugin directory.
    /// </summary>
    public string PluginPath { get; }

    /// <summary>
    /// Gets the absolute file path to the `game/csgo` directory.
    /// </summary>
    public string CSGODirectory { get; }

    /// <summary>
    /// Gets the absolute file path to the game's root directory.
    /// </summary>
    public string GameDirectory { get; }

    /// <summary>
    /// Gets the file path to the plugin data directory.
    /// This directory is ensured to exist by the framework.
    /// </summary>
    public string PluginDataDirectory { get; }

    /// <summary>
    /// Game file system interface.
    /// </summary>
    public IGameFileSystem GameFileSystem { get; }
}