using System.Reflection;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Plugins;
using SwiftlyS2.Shared.Profiler;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.Permissions;

namespace SwiftlyS2.Core.Commands;

internal class CommandService : ICommandService, IDisposable
{

  private List<CommandCallbackBase> _callbacks = new();
  private ILogger<CommandService> _Logger { get; init; }
  private ILoggerFactory _LoggerFactory { get; init; }
  private IContextedProfilerService _Profiler { get; init; }
  private IPlayerManagerService _PlayerManagerService { get; init; }
  private IPermissionManager _PermissionManager { get; init; }

  private Lock _lock = new();

  public CommandService( ILogger<CommandService> logger, ILoggerFactory loggerFactory, IContextedProfilerService profiler, IPlayerManagerService playerManagerService, IPermissionManager permissionManager )
  {
    _Logger = logger;
    _LoggerFactory = loggerFactory;
    _Profiler = profiler;
    _PlayerManagerService = playerManagerService;
    _PermissionManager = permissionManager;
  }

  public Guid RegisterCommand( string commandName, ICommandService.CommandListener handler, bool registerRaw = false, string permission = "" )
  {
    var callback = new CommandCallback(commandName, registerRaw, handler, permission, _PlayerManagerService, _PermissionManager, _LoggerFactory, _Profiler);
    lock (_lock)
    {
      _callbacks.Add(callback);
    }

    return callback.Guid;
  }

  public void RegisterCommandAlias( string commandName, string alias, bool registerRaw = false )
  {
    NativeCommands.RegisterAlias(alias, commandName, registerRaw);
  }

  public void UnregisterCommand( Guid guid )
  {
    lock (_lock)
    {
      _callbacks.RemoveAll(callback =>
      {
        if (callback.Guid == guid)
        {
          callback.Dispose();
          return true;
        }
        return false;
      });
    }
  }

  public void UnregisterCommand( string commandName )
  {
    lock (_lock)
    {
      _callbacks.RemoveAll(callback =>
      {
        if (callback is CommandCallback commandCallback && commandCallback.CommandName == commandName)
        {
          commandCallback.Dispose();
          return true;
        }
        return false;
      });
    }
  }

  public Guid HookClientCommand( ICommandService.ClientCommandHandler handler )
  {
    var callback = new ClientCommandListenerCallback(handler, _LoggerFactory, _Profiler);
    lock (_lock)
    {
      _callbacks.Add(callback);
    }
    return callback.Guid;
  }

  public void UnhookClientCommand( Guid guid )
  {
    lock (_lock)
    {
      _callbacks.RemoveAll(callback =>
      {
        if (callback is ClientCommandListenerCallback clientCommandCallback && clientCommandCallback.Guid == guid)
        {
          clientCommandCallback.Dispose();
          return true;
        }
        return false;
      });
    }
  }

  public Guid HookClientChat( ICommandService.ClientChatHandler handler )
  {
    var callback = new ClientChatListenerCallback(handler, _LoggerFactory, _Profiler);
    lock (_lock)
    {
      _callbacks.Add(callback);
    }
    return callback.Guid;
  }

  public void UnhookClientChat( Guid guid )
  {
    lock (_lock)
    {
      _callbacks.RemoveAll(callback =>
      {
        if (callback is ClientChatListenerCallback clientChatListenerCallback && clientChatListenerCallback.Guid == guid)
        {
          clientChatListenerCallback.Dispose();
          return true;
        }
        return false;
      });
    }
  }

  public void Dispose()
  {
    lock (_lock)
    {
      foreach (var callback in _callbacks)
      {
        callback.Dispose();
      }
      _callbacks.Clear();
    }
  }
}