using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Profiler;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHooksService : IGameHooks, IDisposable
{
    internal readonly GameHookItems ItemsHook = new();
    internal readonly GameHookMovement MovementHook = new();
    internal readonly GameHookPawn PawnHook = new();
    internal readonly GameHookWeapons WeaponsHook = new();
    internal readonly GameHookController ControllerHook = new();
    private bool _disposed = false;
    private readonly IContextedProfilerService profiler;
    private readonly ILogger<GameHooksService> logger;

    public IGameHookItems Items => ItemsHook;
    public IGameHookMovement Movement => MovementHook;
    public IGameHookPawn Pawn => PawnHook;
    public IGameHookWeapons Weapons => WeaponsHook;
    public IGameHookController Controller => ControllerHook;

    public GameHooksService( IContextedProfilerService profiler, ILogger<GameHooksService> logger )
    {
        this.profiler = profiler;
        this.logger = logger;
        GameHooksPublisher.Subscribe(this);
    }

    ~GameHooksService()
    {
        Dispose();
    }

    public void Dispose()
    {
        if (_disposed == true)
            return;

        ControllerHook.ProcessUsercmdsEvents.UnregisterListeners();
        ItemsHook.CanAcquireEvents.UnregisterListeners();

        _disposed = true;
        GameHooksPublisher.Unsubscribe(this);
        GC.SuppressFinalize(this);
    }

    internal void InvokeProcessUsercmdsPre( ref IProcessUsercmdsController @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Controller::ProcessUsercmds::Pre");
            ControllerHook.ProcessUsercmdsEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Controller::ProcessUsercmds::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Controller::ProcessUsercmds::Pre");
        }
    }

    internal void InvokeProcessUsercmdsPost( ref IProcessUsercmdsController @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Controller::ProcessUsercmds::Post");
            ControllerHook.ProcessUsercmdsEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Controller::ProcessUsercmds::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Controller::ProcessUsercmds::Post");
        }
    }

    internal void InvokeCanAcquirePre( ref ICanAcquireItem @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Items::CanAcquire::Pre");
            ItemsHook.CanAcquireEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Items::CanAcquire::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Items::CanAcquire::Pre");
        }
    }

    internal void InvokeCanAcquirePost( ref ICanAcquireItem @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Items::CanAcquire::Post");
            ItemsHook.CanAcquireEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Items::CanAcquire::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Items::CanAcquire::Post");
        }
    }
}
