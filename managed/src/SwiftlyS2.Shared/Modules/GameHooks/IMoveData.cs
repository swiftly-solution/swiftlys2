using SwiftlyS2.Core.GameHooks;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public interface IMoveData
{
    public nint Address { get; }
    public unsafe CMoveData* TypedAddress { get; }
    public static IMoveData From( nint address ) => new CMoveDataImpl { Address = address };

    public bool HasZeroFrametime { get; set; }

    public bool IsLateCommand { get; set; }

    public CHandle<CCSPlayerPawn> PlayerHandle { get; }
    public QAngle AbsViewAngles { get; set; }
    public QAngle ViewAngles { get; set; }
    public Vector LastMovementImpulses { get; set; }
    public float ForwardMove { get; set; }
    // Warning! Flipped compared to CS:GO, moving right gives negative value
    public float SideMove { get; set; }
    public float UpMove { get; set; }
    public Vector Velocity { get; set; }
    public QAngle Angles { get; set; }
    public CUtlVector<SubtickMove> SubtickMoves { get; }
    public CUtlVector<SubtickMove> AttackSubtickMoves { get; }
    public bool HasSubtickInputs { get; set; }
    public CUtlVector<TouchListT> TouchList { get; }
    public Vector CollisionNormal { get; set; }
    public Vector GroundNormal { get; set; }
    public Vector AbsOrigin { get; set; }
    public int TickCount { get; set; }
    public int TargetTick { get; set; }
    public float SubtickStartFraction { get; set; }
    public float SubtickEndFraction { get; set; }

    public Vector OutWishVel { get; set; }
    public QAngle OldAngles { get; set; }
    public Vector2D WalkWishedVelocity { get; set; }
    public Vector Acceleration { get; set; }
    public Vector ContinuousAcceleration { get; set; }
    public float MaxSpeed { get; set; }
    public float ClientMaxSpeed { get; set; }
    public float FrictionDecel { get; set; }
    public bool InAir { get; set; }
    // true if usercmd cmd number == (m_nGameCodeHasMovedPlayerAfterCommand + 1)
    public bool GameCodeMovedPlayer { get; set; }
}
