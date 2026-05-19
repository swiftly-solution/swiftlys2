using SwiftlyS2.Core.GameHooks;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public interface IMoveData
{
    public nint Address { get; }
    public unsafe CMoveData* TypedAddress { get; }
    public static IMoveData From( nint address ) => new CMoveDataImpl { Address = address };

    public bool HasZeroFrametime { get; }

    public bool IsLateCommand { get; }

    public CHandle<CCSPlayerPawn> PlayerHandle { get; }
    public QAngle AbsViewAngles { get; }
    public QAngle ViewAngles { get; }
    public Vector LastMovementImpulses { get; }
    public float ForwardMove { get; }
    // Warning! Flipped compared to CS:GO, moving right gives negative value
    public float SideMove { get; }
    public float UpMove { get; }
    public Vector Velocity { get; }
    public QAngle Angles { get; }
    public CUtlVector<SubtickMove> SubtickMoves { get; }
    public CUtlVector<SubtickMove> AttackSubtickMoves { get; }
    public bool HasSubtickInputs { get; }
    public CUtlVector<TouchListT> TouchList { get; }
    public Vector CollisionNormal { get; }
    public Vector GroundNormal { get; }
    public Vector AbsOrigin { get; }
    public int TickCount { get; }
    public int TargetTick { get; }
    public float SubtickStartFraction { get; }
    public float SubtickEndFraction { get; }

    public Vector OutWishVel { get; }
    public QAngle OldAngles { get; }
    public Vector2D WalkWishedVelocity { get; }
    public Vector Acceleration { get; }
    public Vector ContinuousAcceleration { get; }
    public float MaxSpeed { get; }
    public float ClientMaxSpeed { get; }
    public float FrictionDecel { get; }
    public bool InAir { get; }
    // true if usercmd cmd number == (m_nGameCodeHasMovedPlayerAfterCommand + 1)
    public bool GameCodeMovedPlayer { get; }
}
