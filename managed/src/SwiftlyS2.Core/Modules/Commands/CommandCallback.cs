using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.Profiler;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Permissions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SwiftlyS2.Core.Models;
using SwiftlyS2.Core.Translations;

namespace SwiftlyS2.Core.Commands;

internal delegate void GlobalCommandHandlerDelegate( nint commandName, int playerId, nint args, nint originalCommandName, nint prefix, byte silent );
internal delegate HookResult ClientCommandListenerCallbackDelegate( int playerId, nint commandLine );
internal delegate HookResult ClientChatListenerCallbackDelegate( int playerId, nint text, byte teamonly );

internal abstract class CommandCallbackBase
{
    public Guid Guid { get; } = Guid.NewGuid();
    public string PluginName { get; protected init; }
    public IContextedProfilerService Profiler { get; }
    public ILoggerFactory LoggerFactory { get; }

    protected CommandCallbackBase( ILoggerFactory loggerFactory, IContextedProfilerService profiler, string pluginName )
    {
        LoggerFactory = loggerFactory;
        Profiler = profiler;
        PluginName = pluginName;
    }
}

internal class CommandCallback : CommandCallbackBase
{
    public string CommandName { get; }
    public bool RegisterRaw { get; }
    public string Permission { get; }
    public string HelpText { get; }
    public string NormalizedName { get; }

    private readonly ICommandService.CommandListener commandHandle;
    private readonly ILogger<CommandCallback> logger;
    private readonly IPlayerManagerService playerManagerService;
    private readonly IPermissionManager permissionManager;
    private readonly IOptionsMonitor<CommandOverrideConfig> commandOverrideOptions;

    public CommandCallback( string commandName, bool registerRaw, ICommandService.CommandListener handler, string permission, string helpText, IPlayerManagerService playerManagerService, IPermissionManager permissionManager, IOptionsMonitor<CommandOverrideConfig> commandOverrideOptions, ILoggerFactory loggerFactory, IContextedProfilerService profiler, string pluginName ) : base(loggerFactory, profiler, pluginName)
    {
        this.logger = LoggerFactory.CreateLogger<CommandCallback>();
        this.playerManagerService = playerManagerService;
        this.permissionManager = permissionManager;
        this.commandOverrideOptions = commandOverrideOptions;

        CommandName = commandName;
        RegisterRaw = registerRaw;
        Permission = permission;
        HelpText = helpText;
        NormalizedName = CommandDispatcher.NormalizeName(commandName, registerRaw);
        commandHandle = handler;
    }

    internal void Invoke( int playerId, string[] args, string originalCommandName, string prefix, bool silent )
    {
        try
        {
            var context = new CommandContext(playerId, args, originalCommandName, prefix, silent);
            var hasOverride = commandOverrideOptions.CurrentValue.Permissions.TryGetValue(originalCommandName, out var overriddenPermission);
            var requiredPermission = hasOverride ? overriddenPermission : Permission;
            if (!context.IsSentByPlayer || string.IsNullOrWhiteSpace(requiredPermission) || permissionManager.PlayerHasPermission(playerManagerService.GetPlayer(playerId)?.SteamID ?? 0, requiredPermission))
            {
                commandHandle(context);
            }
            else
            {
                context.Reply(GlobalLocalization.PermissionCommandDenied());
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return;
            logger.LogError(e, "Failed to handle command {CommandName}.", CommandName);
        }
    }
}

internal class ClientCommandListenerCallback : CommandCallbackBase
{
    private readonly ICommandService.ClientCommandHandler commandHandle;
    private readonly ILogger<ClientCommandListenerCallback> logger;

    public ClientCommandListenerCallback( ICommandService.ClientCommandHandler handler, ILoggerFactory loggerFactory, IContextedProfilerService profiler, string pluginName ) : base(loggerFactory, profiler, pluginName)
    {
        logger = LoggerFactory.CreateLogger<ClientCommandListenerCallback>();
        commandHandle = handler;
    }

    internal HookResult Invoke( int playerId, string commandLine )
    {
        try
        {
            return commandHandle(playerId, commandLine);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return HookResult.Continue;
            logger.LogError(e, "Failed to handle client command listener.");
            return HookResult.Continue;
        }
    }
}

internal class ClientChatListenerCallback : CommandCallbackBase
{
    private readonly ICommandService.ClientChatHandler commandHandle;
    private readonly ILogger<ClientChatListenerCallback> logger;

    public ClientChatListenerCallback( ICommandService.ClientChatHandler handler, ILoggerFactory loggerFactory, IContextedProfilerService profiler, string pluginName ) : base(loggerFactory, profiler, pluginName)
    {
        logger = LoggerFactory.CreateLogger<ClientChatListenerCallback>();
        commandHandle = handler;
    }

    internal HookResult Invoke( int playerId, string text, bool teamonly )
    {
        try
        {
            return commandHandle(playerId, text, teamonly);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return HookResult.Continue;
            logger.LogError(e, "Failed to handle client chat listener.");
            return HookResult.Continue;
        }
    }
}
