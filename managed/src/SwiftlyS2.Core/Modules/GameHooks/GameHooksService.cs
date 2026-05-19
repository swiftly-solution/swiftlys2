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
        ControllerHook.SimulateUserCommandsEvents.UnregisterListeners();

        ItemsHook.CanAcquireEvents.UnregisterListeners();

        MovementHook.RunCommandEvents.UnregisterListeners();
        MovementHook.SetupMoveEvents.UnregisterListeners();
        MovementHook.ProcessMovementEvents.UnregisterListeners();

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

    internal void InvokeSetupMovePre( ref ISetupMoveMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::SetupMove::Pre");
            MovementHook.SetupMoveEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::SetupMove::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::SetupMove::Pre");
        }
    }

    internal void InvokeSetupMovePost( ref ISetupMoveMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::SetupMove::Post");
            MovementHook.SetupMoveEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::SetupMove::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::SetupMove::Post");
        }
    }

    internal void InvokeProcessMovementPre( ref IProcessMovementMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::ProcessMovement::Pre");
            MovementHook.ProcessMovementEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::ProcessMovement::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::ProcessMovement::Pre");
        }
    }

    internal void InvokeProcessMovementPost( ref IProcessMovementMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::ProcessMovement::Post");
            MovementHook.ProcessMovementEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::ProcessMovement::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::ProcessMovement::Post");
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

    internal void InvokeSimulateUserCommandsPre( ref ISimulateUserCommandsController @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Controller::SimulateUserCommands::Pre");
            ControllerHook.SimulateUserCommandsEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Controller::SimulateUserCommands::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Controller::SimulateUserCommands::Pre");
        }
    }

    internal void InvokeSimulateUserCommandsPost( ref ISimulateUserCommandsController @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Controller::SimulateUserCommands::Post");
            ControllerHook.SimulateUserCommandsEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Controller::SimulateUserCommands::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Controller::SimulateUserCommands::Post");
        }
    }
}
