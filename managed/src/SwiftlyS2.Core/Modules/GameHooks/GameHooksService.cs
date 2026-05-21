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

        ControllerHook.ProcessUsercmdsHook.UnregisterListeners();
        ControllerHook.SimulateUserCommandsHook.UnregisterListeners();

        ItemsHook.CanAcquireHook.UnregisterListeners();

        MovementHook.RunCommandHook.UnregisterListeners();
        MovementHook.SetupMoveHook.UnregisterListeners();
        MovementHook.ProcessMovementHook.UnregisterListeners();
        MovementHook.CheckFallingHook.UnregisterListeners();
        MovementHook.CategorizePositionHook.UnregisterListeners();
        MovementHook.TryPlayerMoveHook.UnregisterListeners();
        MovementHook.WalkMoveHook.UnregisterListeners();
        MovementHook.FrictionHook.UnregisterListeners();
        MovementHook.AirAccelerateHook.UnregisterListeners();
        MovementHook.AirMoveHook.UnregisterListeners();
        MovementHook.OnJumpModernHook.UnregisterListeners();
        MovementHook.OnJumpLegacyHook.UnregisterListeners();
        MovementHook.CheckJumpButtonModernHook.UnregisterListeners();
        MovementHook.CheckJumpButtonLegacyHook.UnregisterListeners();
        MovementHook.LadderMoveHook.UnregisterListeners();
        MovementHook.CanUnduckHook.UnregisterListeners();
        MovementHook.DuckHook.UnregisterListeners();
        MovementHook.CheckVelocityHook.UnregisterListeners();
        MovementHook.WaterMoveHook.UnregisterListeners();
        MovementHook.CheckWaterHook.UnregisterListeners();
        MovementHook.MoveInitHook.UnregisterListeners();
        MovementHook.FullWalkMoveHook.UnregisterListeners();
        MovementHook.CheckParametersHook.UnregisterListeners();
        MovementHook.PlayerMoveHook.UnregisterListeners();
        MovementHook.GroundAccelerateHook.UnregisterListeners();

        PawnHook.PostThinkHook.UnregisterListeners();
        PawnHook.CanMoveHook.UnregisterListeners();

        WeaponsHook.CanUseHook.UnregisterListeners();
        WeaponsHook.DropHook.UnregisterListeners();

        _disposed = true;
        GameHooksPublisher.Unsubscribe(this);
        GC.SuppressFinalize(this);
    }

    internal void InvokeProcessUsercmdsPre( ref ProcessUsercmdsPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Controller::ProcessUsercmds::Pre");
            ControllerHook.ProcessUsercmdsHook.InvokePre(ref ctx);
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

    internal void InvokeProcessUsercmdsPost( ref ProcessUsercmdsPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Controller::ProcessUsercmds::Post");
            ControllerHook.ProcessUsercmdsHook.InvokePost(ref ctx);
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

    internal void InvokeSimulateUserCommandsPre( ref SimulateUserCommandsPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Controller::SimulateUserCommands::Pre");
            ControllerHook.SimulateUserCommandsHook.InvokePre(ref ctx);
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

    internal void InvokeSimulateUserCommandsPost( ref SimulateUserCommandsPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Controller::SimulateUserCommands::Post");
            ControllerHook.SimulateUserCommandsHook.InvokePost(ref ctx);
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

    internal void InvokeCanAcquirePre( ref CanAcquireItemPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Items::CanAcquire::Pre");
            ItemsHook.CanAcquireHook.InvokePre(ref ctx);
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

    internal void InvokeCanAcquirePost( ref CanAcquireItemPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Items::CanAcquire::Post");
            ItemsHook.CanAcquireHook.InvokePost(ref ctx);
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

    internal void InvokePostThinkPre( ref PostThinkPawnPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Pawn::PostThink::Pre");
            PawnHook.PostThinkHook.InvokePre(ref ctx);
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

    internal void InvokePostThinkPost( ref PostThinkPawnPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Pawn::PostThink::Post");
            PawnHook.PostThinkHook.InvokePost(ref ctx);
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

    internal void InvokeCanMovePre( ref CanMovePawnPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Pawn::CanMove::Pre");
            PawnHook.CanMoveHook.InvokePre(ref ctx);
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

    internal void InvokeCanMovePost( ref CanMovePawnPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Pawn::CanMove::Post");
            PawnHook.CanMoveHook.InvokePost(ref ctx);
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

    internal void InvokeCanUsePre( ref CanUseWeaponPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Weapons::CanUse::Pre");
            WeaponsHook.CanUseHook.InvokePre(ref ctx);
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

    internal void InvokeCanUsePost( ref CanUseWeaponPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Weapons::CanUse::Post");
            WeaponsHook.CanUseHook.InvokePost(ref ctx);
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

    internal void InvokeWeaponDropPre( ref WeaponDropPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Weapons::Drop::Pre");
            WeaponsHook.DropHook.InvokePre(ref ctx);
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

    internal void InvokeWeaponDropPost( ref WeaponDropPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Weapons::Drop::Post");
            WeaponsHook.DropHook.InvokePost(ref ctx);
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

    internal void InvokeRunCommandPre( ref RunCommandMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::RunCommand::Pre");
            MovementHook.RunCommandHook.InvokePre(ref ctx);
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

    internal void InvokeRunCommandPost( ref RunCommandMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::RunCommand::Post");
            MovementHook.RunCommandHook.InvokePost(ref ctx);
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

    internal void InvokeSetupMovePre( ref SetupMoveMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::SetupMove::Pre");
            MovementHook.SetupMoveHook.InvokePre(ref ctx);
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

    internal void InvokeSetupMovePost( ref SetupMoveMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::SetupMove::Post");
            MovementHook.SetupMoveHook.InvokePost(ref ctx);
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

    internal void InvokeProcessMovementPre( ref ProcessMovementMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::ProcessMovement::Pre");
            MovementHook.ProcessMovementHook.InvokePre(ref ctx);
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

    internal void InvokeProcessMovementPost( ref ProcessMovementMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::ProcessMovement::Post");
            MovementHook.ProcessMovementHook.InvokePost(ref ctx);
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

    internal void InvokeCheckFallingPre( ref CheckFallingMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckFalling::Pre");
            MovementHook.CheckFallingHook.InvokePre(ref ctx);
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

    internal void InvokeCheckFallingPost( ref CheckFallingMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckFalling::Post");
            MovementHook.CheckFallingHook.InvokePost(ref ctx);
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

    internal void InvokeCategorizePositionPre( ref CategorizePositionMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CategorizePosition::Pre");
            MovementHook.CategorizePositionHook.InvokePre(ref ctx);
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

    internal void InvokeCategorizePositionPost( ref CategorizePositionMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CategorizePosition::Post");
            MovementHook.CategorizePositionHook.InvokePost(ref ctx);
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

    internal void InvokeTryPlayerMovePre( ref TryPlayerMoveMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::TryPlayerMove::Pre");
            MovementHook.TryPlayerMoveHook.InvokePre(ref ctx);
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

    internal void InvokeTryPlayerMovePost( ref TryPlayerMoveMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::TryPlayerMove::Post");
            MovementHook.TryPlayerMoveHook.InvokePost(ref ctx);
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

    internal void InvokeWalkMovePre( ref WalkMoveMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::WalkMove::Pre");
            MovementHook.WalkMoveHook.InvokePre(ref ctx);
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

    internal void InvokeWalkMovePost( ref WalkMoveMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::WalkMove::Post");
            MovementHook.WalkMoveHook.InvokePost(ref ctx);
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

    internal void InvokeFrictionPre( ref FrictionMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::Friction::Pre");
            MovementHook.FrictionHook.InvokePre(ref ctx);
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

    internal void InvokeFrictionPost( ref FrictionMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::Friction::Post");
            MovementHook.FrictionHook.InvokePost(ref ctx);
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

    internal void InvokeAirAcceleratePre( ref AirAccelerateMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::AirAccelerate::Pre");
            MovementHook.AirAccelerateHook.InvokePre(ref ctx);
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

    internal void InvokeAirAcceleratePost( ref AirAccelerateMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::AirAccelerate::Post");
            MovementHook.AirAccelerateHook.InvokePost(ref ctx);
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

    internal void InvokeAirMovePre( ref AirMoveMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::AirMove::Pre");
            MovementHook.AirMoveHook.InvokePre(ref ctx);
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

    internal void InvokeAirMovePost( ref AirMoveMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::AirMove::Post");
            MovementHook.AirMoveHook.InvokePost(ref ctx);
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

    internal void InvokeOnJumpModernPre( ref OnJumpModernMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::OnJumpModern::Pre");
            MovementHook.OnJumpModernHook.InvokePre(ref ctx);
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

    internal void InvokeOnJumpModernPost( ref OnJumpModernMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::OnJumpModern::Post");
            MovementHook.OnJumpModernHook.InvokePost(ref ctx);
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

    internal void InvokeOnJumpLegacyPre( ref OnJumpLegacyMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::OnJumpLegacy::Pre");
            MovementHook.OnJumpLegacyHook.InvokePre(ref ctx);
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

    internal void InvokeOnJumpLegacyPost( ref OnJumpLegacyMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::OnJumpLegacy::Post");
            MovementHook.OnJumpLegacyHook.InvokePost(ref ctx);
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

    internal void InvokeCheckJumpButtonModernPre( ref CheckJumpButtonModernMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckJumpButtonModern::Pre");
            MovementHook.CheckJumpButtonModernHook.InvokePre(ref ctx);
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

    internal void InvokeCheckJumpButtonModernPost( ref CheckJumpButtonModernMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckJumpButtonModern::Post");
            MovementHook.CheckJumpButtonModernHook.InvokePost(ref ctx);
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

    internal void InvokeCheckJumpButtonLegacyPre( ref CheckJumpButtonLegacyMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckJumpButtonLegacy::Pre");
            MovementHook.CheckJumpButtonLegacyHook.InvokePre(ref ctx);
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

    internal void InvokeCheckJumpButtonLegacyPost( ref CheckJumpButtonLegacyMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckJumpButtonLegacy::Post");
            MovementHook.CheckJumpButtonLegacyHook.InvokePost(ref ctx);
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

    internal void InvokeLadderMovePre( ref LadderMoveMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::LadderMove::Pre");
            MovementHook.LadderMoveHook.InvokePre(ref ctx);
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

    internal void InvokeLadderMovePost( ref LadderMoveMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::LadderMove::Post");
            MovementHook.LadderMoveHook.InvokePost(ref ctx);
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

    internal void InvokeCanUnduckPre( ref CanUnduckMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CanUnduck::Pre");
            MovementHook.CanUnduckHook.InvokePre(ref ctx);
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

    internal void InvokeCanUnduckPost( ref CanUnduckMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CanUnduck::Post");
            MovementHook.CanUnduckHook.InvokePost(ref ctx);
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

    internal void InvokeDuckPre( ref DuckMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::Duck::Pre");
            MovementHook.DuckHook.InvokePre(ref ctx);
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

    internal void InvokeDuckPost( ref DuckMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::Duck::Post");
            MovementHook.DuckHook.InvokePost(ref ctx);
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

    internal void InvokeCheckVelocityPre( ref CheckVelocityMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckVelocity::Pre");
            MovementHook.CheckVelocityHook.InvokePre(ref ctx);
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

    internal void InvokeCheckVelocityPost( ref CheckVelocityMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckVelocity::Post");
            MovementHook.CheckVelocityHook.InvokePost(ref ctx);
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

    internal void InvokeWaterMovePre( ref WaterMoveMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::WaterMove::Pre");
            MovementHook.WaterMoveHook.InvokePre(ref ctx);
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

    internal void InvokeWaterMovePost( ref WaterMoveMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::WaterMove::Post");
            MovementHook.WaterMoveHook.InvokePost(ref ctx);
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

    internal void InvokeCheckWaterPre( ref CheckWaterMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckWater::Pre");
            MovementHook.CheckWaterHook.InvokePre(ref ctx);
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

    internal void InvokeCheckWaterPost( ref CheckWaterMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckWater::Post");
            MovementHook.CheckWaterHook.InvokePost(ref ctx);
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

    internal void InvokeMoveInitPre( ref MoveInitMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::MoveInit::Pre");
            MovementHook.MoveInitHook.InvokePre(ref ctx);
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

    internal void InvokeMoveInitPost( ref MoveInitMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::MoveInit::Post");
            MovementHook.MoveInitHook.InvokePost(ref ctx);
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

    internal void InvokeFullWalkMovePre( ref FullWalkMoveMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::FullWalkMove::Pre");
            MovementHook.FullWalkMoveHook.InvokePre(ref ctx);
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

    internal void InvokeFullWalkMovePost( ref FullWalkMoveMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::FullWalkMove::Post");
            MovementHook.FullWalkMoveHook.InvokePost(ref ctx);
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

    internal void InvokeCheckParametersPre( ref CheckParametersMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckParameters::Pre");
            MovementHook.CheckParametersHook.InvokePre(ref ctx);
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

    internal void InvokeCheckParametersPost( ref CheckParametersMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::CheckParameters::Post");
            MovementHook.CheckParametersHook.InvokePost(ref ctx);
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

    internal void InvokePlayerMovePre( ref PlayerMoveMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::PlayerMove::Pre");
            MovementHook.PlayerMoveHook.InvokePre(ref ctx);
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

    internal void InvokePlayerMovePost( ref PlayerMoveMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::PlayerMove::Post");
            MovementHook.PlayerMoveHook.InvokePost(ref ctx);
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

    internal void InvokeGroundAcceleratePre( ref GroundAccelerateMovementPreContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::GroundAccelerate::Pre");
            MovementHook.GroundAccelerateHook.InvokePre(ref ctx);
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

    internal void InvokeGroundAcceleratePost( ref GroundAccelerateMovementPostContext ctx )
    {
        try
        {
            profiler.StartRecording("GameHooks::Movement::GroundAccelerate::Post");
            MovementHook.GroundAccelerateHook.InvokePost(ref ctx);
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
}
