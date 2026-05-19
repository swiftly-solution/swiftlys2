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
        MovementHook.CheckFallingEvents.UnregisterListeners();
        MovementHook.CategorizePositionEvents.UnregisterListeners();
        MovementHook.TryPlayerMoveEvents.UnregisterListeners();
        MovementHook.WalkMoveEvents.UnregisterListeners();
        MovementHook.FrictionEvents.UnregisterListeners();
        MovementHook.AirAccelerateEvents.UnregisterListeners();
        MovementHook.AirMoveEvents.UnregisterListeners();
        MovementHook.OnJumpModernEvents.UnregisterListeners();
        MovementHook.OnJumpLegacyEvents.UnregisterListeners();
        MovementHook.CheckJumpButtonModernEvents.UnregisterListeners();
        MovementHook.CheckJumpButtonLegacyEvents.UnregisterListeners();
        MovementHook.LadderMoveEvents.UnregisterListeners();
        MovementHook.CanUnduckEvents.UnregisterListeners();
        MovementHook.DuckEvents.UnregisterListeners();
        MovementHook.CheckVelocityEvents.UnregisterListeners();
        MovementHook.WaterMoveEvents.UnregisterListeners();
        MovementHook.CheckWaterEvents.UnregisterListeners();
        MovementHook.MoveInitEvents.UnregisterListeners();
        MovementHook.FullWalkMoveEvents.UnregisterListeners();
        MovementHook.CheckParametersEvents.UnregisterListeners();
        MovementHook.PlayerMoveEvents.UnregisterListeners();
        MovementHook.GroundAccelerateEvents.UnregisterListeners();

        PawnHook.PostThinkEvents.UnregisterListeners();
        PawnHook.CanMoveEvents.UnregisterListeners();

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

    internal void InvokeCheckFallingPre( ref ICheckFallingMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckFalling::Pre");
            MovementHook.CheckFallingEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckFalling::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::CheckFalling::Pre");
        }
    }

    internal void InvokeCheckFallingPost( ref ICheckFallingMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckFalling::Post");
            MovementHook.CheckFallingEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckFalling::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::CheckFalling::Post");
        }
    }

    internal void InvokeCategorizePositionPre( ref ICategorizePositionMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CategorizePosition::Pre");
            MovementHook.CategorizePositionEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CategorizePosition::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::CategorizePosition::Pre");
        }
    }

    internal void InvokeCategorizePositionPost( ref ICategorizePositionMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CategorizePosition::Post");
            MovementHook.CategorizePositionEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CategorizePosition::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::CategorizePosition::Post");
        }
    }

    internal void InvokeTryPlayerMovePre( ref ITryPlayerMoveMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::TryPlayerMove::Pre");
            MovementHook.TryPlayerMoveEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::TryPlayerMove::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::TryPlayerMove::Pre");
        }
    }

    internal void InvokeTryPlayerMovePost( ref ITryPlayerMoveMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::TryPlayerMove::Post");
            MovementHook.TryPlayerMoveEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::TryPlayerMove::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::TryPlayerMove::Post");
        }
    }

    internal void InvokeWalkMovePre( ref IWalkMoveMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::WalkMove::Pre");
            MovementHook.WalkMoveEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::WalkMove::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::WalkMove::Pre");
        }
    }

    internal void InvokeWalkMovePost( ref IWalkMoveMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::WalkMove::Post");
            MovementHook.WalkMoveEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::WalkMove::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::WalkMove::Post");
        }
    }

    internal void InvokeFrictionPre( ref IFrictionMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::Friction::Pre");
            MovementHook.FrictionEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::Friction::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::Friction::Pre");
        }
    }

    internal void InvokeFrictionPost( ref IFrictionMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::Friction::Post");
            MovementHook.FrictionEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::Friction::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::Friction::Post");
        }
    }

    internal void InvokeAirAcceleratePre( ref IAirAccelerateMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::AirAccelerate::Pre");
            MovementHook.AirAccelerateEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::AirAccelerate::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::AirAccelerate::Pre");
        }
    }

    internal void InvokeAirAcceleratePost( ref IAirAccelerateMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::AirAccelerate::Post");
            MovementHook.AirAccelerateEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::AirAccelerate::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::AirAccelerate::Post");
        }
    }

    internal void InvokeAirMovePre( ref IAirMoveMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::AirMove::Pre");
            MovementHook.AirMoveEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::AirMove::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::AirMove::Pre");
        }
    }

    internal void InvokeAirMovePost( ref IAirMoveMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::AirMove::Post");
            MovementHook.AirMoveEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::AirMove::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::AirMove::Post");
        }
    }

    internal void InvokeOnJumpModernPre( ref IOnJumpModernMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::OnJumpModern::Pre");
            MovementHook.OnJumpModernEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::OnJumpModern::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::OnJumpModern::Pre");
        }
    }

    internal void InvokeOnJumpModernPost( ref IOnJumpModernMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::OnJumpModern::Post");
            MovementHook.OnJumpModernEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::OnJumpModern::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::OnJumpModern::Post");
        }
    }

    internal void InvokeOnJumpLegacyPre( ref IOnJumpLegacyMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::OnJumpLegacy::Pre");
            MovementHook.OnJumpLegacyEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::OnJumpLegacy::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::OnJumpLegacy::Pre");
        }
    }

    internal void InvokeOnJumpLegacyPost( ref IOnJumpLegacyMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::OnJumpLegacy::Post");
            MovementHook.OnJumpLegacyEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::OnJumpLegacy::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::OnJumpLegacy::Post");
        }
    }

    internal void InvokeCheckJumpButtonModernPre( ref ICheckJumpButtonModernMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckJumpButtonModern::Pre");
            MovementHook.CheckJumpButtonModernEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckJumpButtonModern::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::CheckJumpButtonModern::Pre");
        }
    }

    internal void InvokeCheckJumpButtonModernPost( ref ICheckJumpButtonModernMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckJumpButtonModern::Post");
            MovementHook.CheckJumpButtonModernEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckJumpButtonModern::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::CheckJumpButtonModern::Post");
        }
    }

    internal void InvokeCheckJumpButtonLegacyPre( ref ICheckJumpButtonLegacyMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckJumpButtonLegacy::Pre");
            MovementHook.CheckJumpButtonLegacyEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckJumpButtonLegacy::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::CheckJumpButtonLegacy::Pre");
        }
    }

    internal void InvokeCheckJumpButtonLegacyPost( ref ICheckJumpButtonLegacyMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckJumpButtonLegacy::Post");
            MovementHook.CheckJumpButtonLegacyEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckJumpButtonLegacy::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::CheckJumpButtonLegacy::Post");
        }
    }

    internal void InvokeLadderMovePre( ref ILadderMoveMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::LadderMove::Pre");
            MovementHook.LadderMoveEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::LadderMove::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::LadderMove::Pre");
        }
    }

    internal void InvokeLadderMovePost( ref ILadderMoveMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::LadderMove::Post");
            MovementHook.LadderMoveEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::LadderMove::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::LadderMove::Post");
        }
    }

    internal void InvokeCanUnduckPre( ref ICanUnduckMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CanUnduck::Pre");
            MovementHook.CanUnduckEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CanUnduck::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::CanUnduck::Pre");
        }
    }

    internal void InvokeCanUnduckPost( ref ICanUnduckMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CanUnduck::Post");
            MovementHook.CanUnduckEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CanUnduck::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::CanUnduck::Post");
        }
    }

    internal void InvokeDuckPre( ref IDuckMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::Duck::Pre");
            MovementHook.DuckEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::Duck::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::Duck::Pre");
        }
    }

    internal void InvokeDuckPost( ref IDuckMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::Duck::Post");
            MovementHook.DuckEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::Duck::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::Duck::Post");
        }
    }

    internal void InvokeCheckVelocityPre( ref ICheckVelocityMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckVelocity::Pre");
            MovementHook.CheckVelocityEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckVelocity::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::CheckVelocity::Pre");
        }
    }

    internal void InvokeCheckVelocityPost( ref ICheckVelocityMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckVelocity::Post");
            MovementHook.CheckVelocityEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckVelocity::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::CheckVelocity::Post");
        }
    }

    internal void InvokeWaterMovePre( ref IWaterMoveMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::WaterMove::Pre");
            MovementHook.WaterMoveEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::WaterMove::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::WaterMove::Pre");
        }
    }

    internal void InvokeWaterMovePost( ref IWaterMoveMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::WaterMove::Post");
            MovementHook.WaterMoveEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::WaterMove::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::WaterMove::Post");
        }
    }

    internal void InvokeCheckWaterPre( ref ICheckWaterMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckWater::Pre");
            MovementHook.CheckWaterEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckWater::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::CheckWater::Pre");
        }
    }

    internal void InvokeCheckWaterPost( ref ICheckWaterMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckWater::Post");
            MovementHook.CheckWaterEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckWater::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::CheckWater::Post");
        }
    }

    internal void InvokeMoveInitPre( ref IMoveInitMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::MoveInit::Pre");
            MovementHook.MoveInitEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::MoveInit::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::MoveInit::Pre");
        }
    }

    internal void InvokeMoveInitPost( ref IMoveInitMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::MoveInit::Post");
            MovementHook.MoveInitEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::MoveInit::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::MoveInit::Post");
        }
    }

    internal void InvokeFullWalkMovePre( ref IFullWalkMoveMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::FullWalkMove::Pre");
            MovementHook.FullWalkMoveEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::FullWalkMove::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::FullWalkMove::Pre");
        }
    }

    internal void InvokeFullWalkMovePost( ref IFullWalkMoveMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::FullWalkMove::Post");
            MovementHook.FullWalkMoveEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::FullWalkMove::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::FullWalkMove::Post");
        }
    }

    internal void InvokeCheckParametersPre( ref ICheckParametersMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckParameters::Pre");
            MovementHook.CheckParametersEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckParameters::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::CheckParameters::Pre");
        }
    }

    internal void InvokeCheckParametersPost( ref ICheckParametersMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckParameters::Post");
            MovementHook.CheckParametersEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckParameters::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::CheckParameters::Post");
        }
    }

    internal void InvokePlayerMovePre( ref IPlayerMoveMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::PlayerMove::Pre");
            MovementHook.PlayerMoveEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::PlayerMove::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::PlayerMove::Pre");
        }
    }

    internal void InvokePlayerMovePost( ref IPlayerMoveMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::PlayerMove::Post");
            MovementHook.PlayerMoveEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::PlayerMove::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::PlayerMove::Post");
        }
    }

    internal void InvokeGroundAcceleratePre( ref IGroundAccelerateMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::GroundAccelerate::Pre");
            MovementHook.GroundAccelerateEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::GroundAccelerate::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::GroundAccelerate::Pre");
        }
    }

    internal void InvokeGroundAcceleratePost( ref IGroundAccelerateMovement @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::GroundAccelerate::Post");
            MovementHook.GroundAccelerateEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::GroundAccelerate::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Movement::GroundAccelerate::Post");
        }
    }

    internal void InvokeCanMovePre( ref ICanMovePawn @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Pawn::CanMove::Pre");
            PawnHook.CanMoveEvents.InvokePre(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Pawn::CanMove::Pre.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Pawn::CanMove::Pre");
        }
    }

    internal void InvokeCanMovePost( ref ICanMovePawn @event )
    {
        try
        {
            profiler.StartRecording("GameHooks::Pawn::CanMove::Post");
            PawnHook.CanMoveEvents.InvokePost(ref @event);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Pawn::CanMove::Post.");
            }
        }
        finally
        {
            profiler.StopRecording("GameHooks::Pawn::CanMove::Post");
        }
    }
}
