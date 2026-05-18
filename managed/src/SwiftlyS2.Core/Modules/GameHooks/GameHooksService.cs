using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Profiler;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHooksService : IGameHooks, IDisposable
{
    internal readonly GameHookItem ItemsHook = new();
    internal readonly GameHookMovement MovementHook = new();
    internal readonly GameHookPawn PawnHook = new();
    internal readonly GameHookWeapon WeaponsHook = new();
    internal readonly GameHookController ControllerHook = new();
    private bool _disposed = false;
    private readonly IContextedProfilerService profiler;
    private readonly ILogger<GameHooksService> logger;

    public IGameHookItem Items => ItemsHook;
    public IGameHookMovement Movement => MovementHook;
    public IGameHookPawn Pawn => PawnHook;
    public IGameHookWeapon Weapons => WeaponsHook;
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
        MovementHook.RunCommandEvents.UnregisterListeners();
        PawnHook.PostThinkEvents.UnregisterListeners();
        WeaponsHook.CanUseEvents.UnregisterListeners();
        WeaponsHook.DropEvents.UnregisterListeners();

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

    internal void InvokeRunCommandPre( ref IRunCommandMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::RunCommand::Pre");
            MovementHook.RunCommandEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::RunCommand::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::RunCommand::Pre");
        }
    }

    internal void InvokeRunCommandPost( ref IRunCommandMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::RunCommand::Post");
            MovementHook.RunCommandEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::RunCommand::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::RunCommand::Post");
        }
    }

    internal void InvokePostThinkPre( ref IPostThinkPawn @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Pawn::PostThink::Pre");
            PawnHook.PostThinkEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Pawn::PostThink::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Pawn::PostThink::Pre");
        }
    }

    internal void InvokePostThinkPost( ref IPostThinkPawn @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Pawn::PostThink::Post");
            PawnHook.PostThinkEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Pawn::PostThink::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Pawn::PostThink::Post");
        }
    }

    internal void InvokeCanUsePre( ref ICanUseWeapon @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Weapons::CanUse::Pre");
            WeaponsHook.CanUseEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Weapons::CanUse::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Weapons::CanUse::Pre");
        }
    }

    internal void InvokeCanUsePost( ref ICanUseWeapon @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Weapons::CanUse::Post");
            WeaponsHook.CanUseEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Weapons::CanUse::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Weapons::CanUse::Post");
        }
    }

    internal void InvokeWeaponDropPre( ref IWeaponDrop @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Weapons::Drop::Pre");
            WeaponsHook.DropEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Weapons::Drop::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Weapons::Drop::Pre");
        }
    }

    internal void InvokeWeaponDropPost( ref IWeaponDrop @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Weapons::Drop::Post");
            WeaponsHook.DropEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Weapons::Drop::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Weapons::Drop::Post");
        }
    }
}
