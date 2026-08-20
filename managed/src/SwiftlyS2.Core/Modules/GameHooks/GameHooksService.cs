using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Profiler;

namespace SwiftlyS2.Core.GameHooks;

internal sealed partial class GameHooksService : IGameHooks, IDisposable
{
    internal readonly GameHookItem ItemsHook = new();
    internal readonly GameHookMovement MovementHook = new();
    internal readonly GameHookPawn PawnHook = new();
    internal readonly GameHookWeapon WeaponsHook = new();
    internal readonly GameHookController ControllerHook = new();
    internal readonly GameHookEntities EntitiesHook = new();
    private bool _disposed = false;
    private readonly IContextedProfilerService profiler;
    private readonly ILogger<GameHooksService> logger;

    public IGameHookItem Items => ItemsHook;
    public IGameHookMovement Movement => MovementHook;
    public IGameHookPawn Pawn => PawnHook;
    public IGameHookWeapon Weapons => WeaponsHook;
    public IGameHookController Controller => ControllerHook;
    public IGameHookEntities Entities => EntitiesHook;

    public GameHooksService( IContextedProfilerService profiler, ILogger<GameHooksService> logger )
    {
        this.profiler = profiler;
        this.logger = logger;
        GameHooksPublisher.Subscribe(this);
        SubscribeDatamapsHooks();
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

        EntitiesHook.TakeDamageHook.UnregisterListeners();
        EntitiesHook.StartTouchHook.UnregisterListeners();
        EntitiesHook.TouchHook.UnregisterListeners();
        EntitiesHook.EndTouchHook.UnregisterListeners();
        EntitiesHook.AcceptInputHook.UnregisterListeners();
        EntitiesHook.FireOutputHook.UnregisterListeners();

        DisposeDatamapsHooks();

        _disposed = true;
        GameHooksPublisher.Unsubscribe(this);
        GC.SuppressFinalize(this);
    }

    internal void InvokeProcessUsercmdsPre( ref ProcessUsercmdsPreContext ctx )
    {
        if (!ControllerHook.ProcessUsercmdsHook.HasPreListeners) return;

        try
        {

            ControllerHook.ProcessUsercmdsHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Controller::ProcessUsercmds::Pre.");
            }
        }

    }

    internal void InvokeProcessUsercmdsPost( ref ProcessUsercmdsPostContext ctx )
    {
        if (!ControllerHook.ProcessUsercmdsHook.HasPostListeners) return;

        try
        {

            ControllerHook.ProcessUsercmdsHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Controller::ProcessUsercmds::Post.");
            }
        }

    }

    internal void InvokeSimulateUserCommandsPre( ref SimulateUserCommandsPreContext ctx )
    {
        if (!ControllerHook.SimulateUserCommandsHook.HasPreListeners) return;

        try
        {

            ControllerHook.SimulateUserCommandsHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Controller::SimulateUserCommands::Pre.");
            }
        }

    }

    internal void InvokeSimulateUserCommandsPost( ref SimulateUserCommandsPostContext ctx )
    {
        if (!ControllerHook.SimulateUserCommandsHook.HasPostListeners) return;

        try
        {

            ControllerHook.SimulateUserCommandsHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Controller::SimulateUserCommands::Post.");
            }
        }

    }

    internal void InvokeCanAcquirePre( ref CanAcquireItemPreContext ctx )
    {
        if (!ItemsHook.CanAcquireHook.HasPreListeners) return;

        try
        {

            ItemsHook.CanAcquireHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Items::CanAcquire::Pre.");
            }
        }

    }

    internal void InvokeCanAcquirePost( ref CanAcquireItemPostContext ctx )
    {
        if (!ItemsHook.CanAcquireHook.HasPostListeners) return;

        try
        {

            ItemsHook.CanAcquireHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Items::CanAcquire::Post.");
            }
        }

    }

    internal void InvokePostThinkPre( ref PostThinkPawnPreContext ctx )
    {
        if (!PawnHook.PostThinkHook.HasPreListeners) return;

        try
        {

            PawnHook.PostThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Pawn::PostThink::Pre.");
            }
        }

    }

    internal void InvokePostThinkPost( ref PostThinkPawnPostContext ctx )
    {
        if (!PawnHook.PostThinkHook.HasPostListeners) return;

        try
        {

            PawnHook.PostThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Pawn::PostThink::Post.");
            }
        }

    }

    internal void InvokeCanMovePre( ref CanMovePawnPreContext ctx )
    {
        if (!PawnHook.CanMoveHook.HasPreListeners) return;

        try
        {

            PawnHook.CanMoveHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Pawn::CanMove::Pre.");
            }
        }

    }

    internal void InvokeCanMovePost( ref CanMovePawnPostContext ctx )
    {
        if (!PawnHook.CanMoveHook.HasPostListeners) return;

        try
        {

            PawnHook.CanMoveHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Pawn::CanMove::Post.");
            }
        }

    }

    internal void InvokeCanUsePre( ref CanUseWeaponPreContext ctx )
    {
        if (!WeaponsHook.CanUseHook.HasPreListeners) return;

        try
        {

            WeaponsHook.CanUseHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Weapons::CanUse::Pre.");
            }
        }

    }

    internal void InvokeCanUsePost( ref CanUseWeaponPostContext ctx )
    {
        if (!WeaponsHook.CanUseHook.HasPostListeners) return;

        try
        {

            WeaponsHook.CanUseHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Weapons::CanUse::Post.");
            }
        }

    }

    internal void InvokeWeaponDropPre( ref WeaponDropPreContext ctx )
    {
        if (!WeaponsHook.DropHook.HasPreListeners) return;

        try
        {

            WeaponsHook.DropHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Weapons::Drop::Pre.");
            }
        }

    }

    internal void InvokeWeaponDropPost( ref WeaponDropPostContext ctx )
    {
        if (!WeaponsHook.DropHook.HasPostListeners) return;

        try
        {

            WeaponsHook.DropHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Weapons::Drop::Post.");
            }
        }

    }

    internal void InvokeRunCommandPre( ref RunCommandMovementPreContext ctx )
    {
        if (!MovementHook.RunCommandHook.HasPreListeners) return;

        try
        {

            MovementHook.RunCommandHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::RunCommand::Pre.");
            }
        }

    }

    internal void InvokeRunCommandPost( ref RunCommandMovementPostContext ctx )
    {
        if (!MovementHook.RunCommandHook.HasPostListeners) return;

        try
        {

            MovementHook.RunCommandHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::RunCommand::Post.");
            }
        }

    }

    internal void InvokeSetupMovePre( ref SetupMoveMovementPreContext ctx )
    {
        if (!MovementHook.SetupMoveHook.HasPreListeners) return;

        try
        {

            MovementHook.SetupMoveHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::SetupMove::Pre.");
            }
        }

    }

    internal void InvokeSetupMovePost( ref SetupMoveMovementPostContext ctx )
    {
        if (!MovementHook.SetupMoveHook.HasPostListeners) return;

        try
        {

            MovementHook.SetupMoveHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::SetupMove::Post.");
            }
        }

    }

    internal void InvokeProcessMovementPre( ref ProcessMovementMovementPreContext ctx )
    {
        if (!MovementHook.ProcessMovementHook.HasPreListeners) return;

        try
        {

            MovementHook.ProcessMovementHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::ProcessMovement::Pre.");
            }
        }

    }

    internal void InvokeProcessMovementPost( ref ProcessMovementMovementPostContext ctx )
    {
        if (!MovementHook.ProcessMovementHook.HasPostListeners) return;

        try
        {

            MovementHook.ProcessMovementHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::ProcessMovement::Post.");
            }
        }

    }

    internal void InvokeCheckFallingPre( ref CheckFallingMovementPreContext ctx )
    {
        if (!MovementHook.CheckFallingHook.HasPreListeners) return;

        try
        {

            MovementHook.CheckFallingHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckFalling::Pre.");
            }
        }

    }

    internal void InvokeCheckFallingPost( ref CheckFallingMovementPostContext ctx )
    {
        if (!MovementHook.CheckFallingHook.HasPostListeners) return;

        try
        {

            MovementHook.CheckFallingHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckFalling::Post.");
            }
        }

    }

    internal void InvokeCategorizePositionPre( ref CategorizePositionMovementPreContext ctx )
    {
        if (!MovementHook.CategorizePositionHook.HasPreListeners) return;

        try
        {

            MovementHook.CategorizePositionHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CategorizePosition::Pre.");
            }
        }

    }

    internal void InvokeCategorizePositionPost( ref CategorizePositionMovementPostContext ctx )
    {
        if (!MovementHook.CategorizePositionHook.HasPostListeners) return;

        try
        {

            MovementHook.CategorizePositionHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CategorizePosition::Post.");
            }
        }

    }

    internal void InvokeTryPlayerMovePre( ref TryPlayerMoveMovementPreContext ctx )
    {
        if (!MovementHook.TryPlayerMoveHook.HasPreListeners) return;

        try
        {

            MovementHook.TryPlayerMoveHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::TryPlayerMove::Pre.");
            }
        }

    }

    internal void InvokeTryPlayerMovePost( ref TryPlayerMoveMovementPostContext ctx )
    {
        if (!MovementHook.TryPlayerMoveHook.HasPostListeners) return;

        try
        {

            MovementHook.TryPlayerMoveHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::TryPlayerMove::Post.");
            }
        }

    }

    internal void InvokeWalkMovePre( ref WalkMoveMovementPreContext ctx )
    {
        if (!MovementHook.WalkMoveHook.HasPreListeners) return;

        try
        {

            MovementHook.WalkMoveHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::WalkMove::Pre.");
            }
        }

    }

    internal void InvokeWalkMovePost( ref WalkMoveMovementPostContext ctx )
    {
        if (!MovementHook.WalkMoveHook.HasPostListeners) return;

        try
        {

            MovementHook.WalkMoveHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::WalkMove::Post.");
            }
        }

    }

    internal void InvokeFrictionPre( ref FrictionMovementPreContext ctx )
    {
        if (!MovementHook.FrictionHook.HasPreListeners) return;

        try
        {

            MovementHook.FrictionHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::Friction::Pre.");
            }
        }

    }

    internal void InvokeFrictionPost( ref FrictionMovementPostContext ctx )
    {
        if (!MovementHook.FrictionHook.HasPostListeners) return;

        try
        {

            MovementHook.FrictionHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::Friction::Post.");
            }
        }

    }

    internal void InvokeAirAcceleratePre( ref AirAccelerateMovementPreContext ctx )
    {
        if (!MovementHook.AirAccelerateHook.HasPreListeners) return;

        try
        {

            MovementHook.AirAccelerateHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::AirAccelerate::Pre.");
            }
        }

    }

    internal void InvokeAirAcceleratePost( ref AirAccelerateMovementPostContext ctx )
    {
        if (!MovementHook.AirAccelerateHook.HasPostListeners) return;

        try
        {

            MovementHook.AirAccelerateHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::AirAccelerate::Post.");
            }
        }

    }

    internal void InvokeAirMovePre( ref AirMoveMovementPreContext ctx )
    {
        if (!MovementHook.AirMoveHook.HasPreListeners) return;

        try
        {

            MovementHook.AirMoveHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::AirMove::Pre.");
            }
        }

    }

    internal void InvokeAirMovePost( ref AirMoveMovementPostContext ctx )
    {
        if (!MovementHook.AirMoveHook.HasPostListeners) return;

        try
        {

            MovementHook.AirMoveHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::AirMove::Post.");
            }
        }

    }

    internal void InvokeOnJumpModernPre( ref OnJumpModernMovementPreContext ctx )
    {
        if (!MovementHook.OnJumpModernHook.HasPreListeners) return;

        try
        {

            MovementHook.OnJumpModernHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::OnJumpModern::Pre.");
            }
        }

    }

    internal void InvokeOnJumpModernPost( ref OnJumpModernMovementPostContext ctx )
    {
        if (!MovementHook.OnJumpModernHook.HasPostListeners) return;

        try
        {

            MovementHook.OnJumpModernHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::OnJumpModern::Post.");
            }
        }

    }

    internal void InvokeOnJumpLegacyPre( ref OnJumpLegacyMovementPreContext ctx )
    {
        if (!MovementHook.OnJumpLegacyHook.HasPreListeners) return;

        try
        {

            MovementHook.OnJumpLegacyHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::OnJumpLegacy::Pre.");
            }
        }

    }

    internal void InvokeOnJumpLegacyPost( ref OnJumpLegacyMovementPostContext ctx )
    {
        if (!MovementHook.OnJumpLegacyHook.HasPostListeners) return;

        try
        {

            MovementHook.OnJumpLegacyHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::OnJumpLegacy::Post.");
            }
        }

    }

    internal void InvokeCheckJumpButtonModernPre( ref CheckJumpButtonModernMovementPreContext ctx )
    {
        if (!MovementHook.CheckJumpButtonModernHook.HasPreListeners) return;

        try
        {

            MovementHook.CheckJumpButtonModernHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckJumpButtonModern::Pre.");
            }
        }

    }

    internal void InvokeCheckJumpButtonModernPost( ref CheckJumpButtonModernMovementPostContext ctx )
    {
        if (!MovementHook.CheckJumpButtonModernHook.HasPostListeners) return;

        try
        {

            MovementHook.CheckJumpButtonModernHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckJumpButtonModern::Post.");
            }
        }

    }

    internal void InvokeCheckJumpButtonLegacyPre( ref CheckJumpButtonLegacyMovementPreContext ctx )
    {
        if (!MovementHook.CheckJumpButtonLegacyHook.HasPreListeners) return;

        try
        {

            MovementHook.CheckJumpButtonLegacyHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckJumpButtonLegacy::Pre.");
            }
        }

    }

    internal void InvokeCheckJumpButtonLegacyPost( ref CheckJumpButtonLegacyMovementPostContext ctx )
    {
        if (!MovementHook.CheckJumpButtonLegacyHook.HasPostListeners) return;

        try
        {

            MovementHook.CheckJumpButtonLegacyHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckJumpButtonLegacy::Post.");
            }
        }

    }

    internal void InvokeLadderMovePre( ref LadderMoveMovementPreContext ctx )
    {
        if (!MovementHook.LadderMoveHook.HasPreListeners) return;

        try
        {

            MovementHook.LadderMoveHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::LadderMove::Pre.");
            }
        }

    }

    internal void InvokeLadderMovePost( ref LadderMoveMovementPostContext ctx )
    {
        if (!MovementHook.LadderMoveHook.HasPostListeners) return;

        try
        {

            MovementHook.LadderMoveHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::LadderMove::Post.");
            }
        }

    }

    internal void InvokeCanUnduckPre( ref CanUnduckMovementPreContext ctx )
    {
        if (!MovementHook.CanUnduckHook.HasPreListeners) return;

        try
        {

            MovementHook.CanUnduckHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CanUnduck::Pre.");
            }
        }

    }

    internal void InvokeCanUnduckPost( ref CanUnduckMovementPostContext ctx )
    {
        if (!MovementHook.CanUnduckHook.HasPostListeners) return;

        try
        {

            MovementHook.CanUnduckHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CanUnduck::Post.");
            }
        }

    }

    internal void InvokeDuckPre( ref DuckMovementPreContext ctx )
    {
        if (!MovementHook.DuckHook.HasPreListeners) return;

        try
        {

            MovementHook.DuckHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::Duck::Pre.");
            }
        }

    }

    internal void InvokeDuckPost( ref DuckMovementPostContext ctx )
    {
        if (!MovementHook.DuckHook.HasPostListeners) return;

        try
        {

            MovementHook.DuckHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::Duck::Post.");
            }
        }

    }

    internal void InvokeCheckVelocityPre( ref CheckVelocityMovementPreContext ctx )
    {
        if (!MovementHook.CheckVelocityHook.HasPreListeners) return;

        try
        {

            MovementHook.CheckVelocityHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckVelocity::Pre.");
            }
        }

    }

    internal void InvokeCheckVelocityPost( ref CheckVelocityMovementPostContext ctx )
    {
        if (!MovementHook.CheckVelocityHook.HasPostListeners) return;

        try
        {

            MovementHook.CheckVelocityHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckVelocity::Post.");
            }
        }

    }

    internal void InvokeWaterMovePre( ref WaterMoveMovementPreContext ctx )
    {
        if (!MovementHook.WaterMoveHook.HasPreListeners) return;

        try
        {

            MovementHook.WaterMoveHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::WaterMove::Pre.");
            }
        }

    }

    internal void InvokeWaterMovePost( ref WaterMoveMovementPostContext ctx )
    {
        if (!MovementHook.WaterMoveHook.HasPostListeners) return;

        try
        {

            MovementHook.WaterMoveHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::WaterMove::Post.");
            }
        }

    }

    internal void InvokeCheckWaterPre( ref CheckWaterMovementPreContext ctx )
    {
        if (!MovementHook.CheckWaterHook.HasPreListeners) return;

        try
        {

            MovementHook.CheckWaterHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckWater::Pre.");
            }
        }

    }

    internal void InvokeCheckWaterPost( ref CheckWaterMovementPostContext ctx )
    {
        if (!MovementHook.CheckWaterHook.HasPostListeners) return;

        try
        {

            MovementHook.CheckWaterHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckWater::Post.");
            }
        }

    }

    internal void InvokeMoveInitPre( ref MoveInitMovementPreContext ctx )
    {
        if (!MovementHook.MoveInitHook.HasPreListeners) return;

        try
        {

            MovementHook.MoveInitHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::MoveInit::Pre.");
            }
        }

    }

    internal void InvokeMoveInitPost( ref MoveInitMovementPostContext ctx )
    {
        if (!MovementHook.MoveInitHook.HasPostListeners) return;

        try
        {

            MovementHook.MoveInitHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::MoveInit::Post.");
            }
        }

    }

    internal void InvokeFullWalkMovePre( ref FullWalkMoveMovementPreContext ctx )
    {
        if (!MovementHook.FullWalkMoveHook.HasPreListeners) return;

        try
        {

            MovementHook.FullWalkMoveHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::FullWalkMove::Pre.");
            }
        }

    }

    internal void InvokeFullWalkMovePost( ref FullWalkMoveMovementPostContext ctx )
    {
        if (!MovementHook.FullWalkMoveHook.HasPostListeners) return;

        try
        {

            MovementHook.FullWalkMoveHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::FullWalkMove::Post.");
            }
        }

    }

    internal void InvokeCheckParametersPre( ref CheckParametersMovementPreContext ctx )
    {
        if (!MovementHook.CheckParametersHook.HasPreListeners) return;

        try
        {

            MovementHook.CheckParametersHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckParameters::Pre.");
            }
        }

    }

    internal void InvokeCheckParametersPost( ref CheckParametersMovementPostContext ctx )
    {
        if (!MovementHook.CheckParametersHook.HasPostListeners) return;

        try
        {

            MovementHook.CheckParametersHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::CheckParameters::Post.");
            }
        }

    }

    internal void InvokePlayerMovePre( ref PlayerMoveMovementPreContext ctx )
    {
        if (!MovementHook.PlayerMoveHook.HasPreListeners) return;

        try
        {

            MovementHook.PlayerMoveHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::PlayerMove::Pre.");
            }
        }

    }

    internal void InvokePlayerMovePost( ref PlayerMoveMovementPostContext ctx )
    {
        if (!MovementHook.PlayerMoveHook.HasPostListeners) return;

        try
        {

            MovementHook.PlayerMoveHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::PlayerMove::Post.");
            }
        }

    }

    internal void InvokeGroundAcceleratePre( ref GroundAccelerateMovementPreContext ctx )
    {
        if (!MovementHook.GroundAccelerateHook.HasPreListeners) return;

        try
        {

            MovementHook.GroundAccelerateHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::GroundAccelerate::Pre.");
            }
        }

    }

    internal void InvokeGroundAcceleratePost( ref GroundAccelerateMovementPostContext ctx )
    {
        if (!MovementHook.GroundAccelerateHook.HasPostListeners) return;

        try
        {

            MovementHook.GroundAccelerateHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Movement::GroundAccelerate::Post.");
            }
        }

    }

    internal void InvokeTakeDamagePre( ref TakeDamageEntityPreContext ctx )
    {
        if (!EntitiesHook.TakeDamageHook.HasPreListeners) return;

        try
        {

            EntitiesHook.TakeDamageHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Entities::TakeDamage::Pre.");
            }
        }

    }

    internal void InvokeTakeDamagePost( ref TakeDamageEntityPostContext ctx )
    {
        if (!EntitiesHook.TakeDamageHook.HasPostListeners) return;

        try
        {

            EntitiesHook.TakeDamageHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Entities::TakeDamage::Post.");
            }
        }

    }

    internal void InvokeStartTouchPre( ref StartTouchEntityPreContext ctx )
    {
        if (!EntitiesHook.StartTouchHook.HasPreListeners) return;

        try
        {

            EntitiesHook.StartTouchHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Entities::StartTouch::Pre.");
            }
        }

    }

    internal void InvokeStartTouchPost( ref StartTouchEntityPostContext ctx )
    {
        if (!EntitiesHook.StartTouchHook.HasPostListeners) return;

        try
        {

            EntitiesHook.StartTouchHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Entities::StartTouch::Post.");
            }
        }

    }

    internal void InvokeTouchPre( ref TouchEntityPreContext ctx )
    {
        if (!EntitiesHook.TouchHook.HasPreListeners) return;

        try
        {

            EntitiesHook.TouchHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Entities::Touch::Pre.");
            }
        }

    }

    internal void InvokeTouchPost( ref TouchEntityPostContext ctx )
    {
        if (!EntitiesHook.TouchHook.HasPostListeners) return;

        try
        {

            EntitiesHook.TouchHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Entities::Touch::Post.");
            }
        }

    }

    internal void InvokeEndTouchPre( ref EndTouchEntityPreContext ctx )
    {
        if (!EntitiesHook.EndTouchHook.HasPreListeners) return;

        try
        {

            EntitiesHook.EndTouchHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Entities::EndTouch::Pre.");
            }
        }

    }

    internal void InvokeEndTouchPost( ref EndTouchEntityPostContext ctx )
    {
        if (!EntitiesHook.EndTouchHook.HasPostListeners) return;

        try
        {

            EntitiesHook.EndTouchHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Entities::EndTouch::Post.");
            }
        }

    }

    internal void InvokeAcceptInputPre( ref AcceptInputEntityPreContext ctx )
    {
        if (!EntitiesHook.AcceptInputHook.HasPreListeners) return;

        try
        {

            EntitiesHook.AcceptInputHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Entities::AcceptInput::Pre.");
            }
        }

    }

    internal void InvokeAcceptInputPost( ref AcceptInputEntityPostContext ctx )
    {
        if (!EntitiesHook.AcceptInputHook.HasPostListeners) return;

        try
        {

            EntitiesHook.AcceptInputHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Entities::AcceptInput::Post.");
            }
        }

    }

    internal void InvokeFireOutputPre( ref FireOutputEntityPreContext ctx )
    {
        if (!EntitiesHook.FireOutputHook.HasPreListeners) return;

        try
        {

            EntitiesHook.FireOutputHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Entities::FireOutput::Pre.");
            }
        }

    }

    internal void InvokeFireOutputPost( ref FireOutputEntityPostContext ctx )
    {
        if (!EntitiesHook.FireOutputHook.HasPostListeners) return;

        try
        {

            EntitiesHook.FireOutputHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Entities::FireOutput::Post.");
            }
        }

    }
}
