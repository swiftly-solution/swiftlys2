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
        ItemsHook.GetItemInLoadoutHook.UnregisterListeners();

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

    internal void InvokeGetItemInLoadoutPre( ref GetItemInLoadoutPreContext ctx )
    {
        try
        {
            ItemsHook.GetItemInLoadoutHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Items::GetItemInLoadout::Pre.");
            }
        }
    }

    internal void InvokeGetItemInLoadoutPost( ref GetItemInLoadoutPostContext ctx )
    {
        try
        {
            ItemsHook.GetItemInLoadoutHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Items::GetItemInLoadout::Post.");
            }
        }
    }

    internal void InvokePostThinkPre( ref PostThinkPawnPreContext ctx )
    {
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
