using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.GameHooks;

internal class CMoveDataImpl : IMoveData, IDisposable
{
    public nint Address { get; init; } = 0;

    public unsafe CMoveData* TypedAddress { get { ThrowIfInvalid(); return (CMoveData*)Address; } }

    private bool _disposed = false;

    ~CMoveDataImpl()
    {
        Dispose();
    }

    public void Dispose()
    {
        if (_disposed) return;

        _disposed = true;
        GC.SuppressFinalize(this);
    }

    private void ThrowIfInvalid()
    {
        if (!_disposed)
        {
            if (Address == 0)
                throw new InvalidOperationException("IMoveData is not valid.");
        }
        else throw new ObjectDisposedException(nameof(IMoveData));
    }

    public unsafe bool HasZeroFrametime { get { ThrowIfInvalid(); return TypedAddress->Base.HasZeroFrametime; } set { ThrowIfInvalid(); TypedAddress->Base.HasZeroFrametime = value; } }
    public unsafe bool IsLateCommand { get { ThrowIfInvalid(); return TypedAddress->Base.IsLateCommand; } set { ThrowIfInvalid(); TypedAddress->Base.IsLateCommand = value; } }

    public unsafe CHandle<CCSPlayerPawn> PlayerHandle { get { ThrowIfInvalid(); return TypedAddress->Base.PlayerHandle; } }
    public unsafe QAngle AbsViewAngles { get { ThrowIfInvalid(); return TypedAddress->Base.AbsViewAngles; } set { ThrowIfInvalid(); TypedAddress->Base.AbsViewAngles = value; } }
    public unsafe QAngle ViewAngles { get { ThrowIfInvalid(); return TypedAddress->Base.ViewAngles; } set { ThrowIfInvalid(); TypedAddress->Base.ViewAngles = value; } }
    public unsafe Vector LastMovementImpulses { get { ThrowIfInvalid(); return TypedAddress->Base.LastMovementImpulses; } set { ThrowIfInvalid(); TypedAddress->Base.LastMovementImpulses = value; } }
    public unsafe float ForwardMove { get { ThrowIfInvalid(); return TypedAddress->Base.ForwardMove; } set { ThrowIfInvalid(); TypedAddress->Base.ForwardMove = value; } }
    // Warning! Flipped compared to CS:GO, moving right gives negative value
    public unsafe float SideMove { get { ThrowIfInvalid(); return TypedAddress->Base.SideMove; } set { ThrowIfInvalid(); TypedAddress->Base.SideMove = value; } }
    public unsafe float UpMove { get { ThrowIfInvalid(); return TypedAddress->Base.UpMove; } set { ThrowIfInvalid(); TypedAddress->Base.UpMove = value; } }
    public unsafe Vector Velocity { get { ThrowIfInvalid(); return TypedAddress->Base.Velocity; } set { ThrowIfInvalid(); TypedAddress->Base.Velocity = value; } }
    public unsafe QAngle Angles { get { ThrowIfInvalid(); return TypedAddress->Base.Angles; } set { ThrowIfInvalid(); TypedAddress->Base.Angles = value; } }
    public unsafe CUtlVector<SubtickMove> SubtickMoves { get { ThrowIfInvalid(); return TypedAddress->Base.SubtickMoves; } }
    public unsafe CUtlVector<SubtickMove> AttackSubtickMoves { get { ThrowIfInvalid(); return TypedAddress->Base.AttackSubtickMoves; } }
    public unsafe bool HasSubtickInputs { get { ThrowIfInvalid(); return TypedAddress->Base.HasSubtickInputs; } set { ThrowIfInvalid(); TypedAddress->Base.HasSubtickInputs = value; } }
    public unsafe CUtlVector<TouchListT> TouchList { get { ThrowIfInvalid(); return TypedAddress->Base.TouchList; } }
    public unsafe Vector CollisionNormal { get { ThrowIfInvalid(); return TypedAddress->Base.CollisionNormal; } set { ThrowIfInvalid(); TypedAddress->Base.CollisionNormal = value; } }
    public unsafe Vector GroundNormal { get { ThrowIfInvalid(); return TypedAddress->Base.GroundNormal; } set { ThrowIfInvalid(); TypedAddress->Base.GroundNormal = value; } }
    public unsafe Vector AbsOrigin { get { ThrowIfInvalid(); return TypedAddress->Base.AbsOrigin; } set { ThrowIfInvalid(); TypedAddress->Base.AbsOrigin = value; } }
    public unsafe int TickCount { get { ThrowIfInvalid(); return TypedAddress->Base.TickCount; } set { ThrowIfInvalid(); TypedAddress->Base.TickCount = value; } }
    public unsafe int TargetTick { get { ThrowIfInvalid(); return TypedAddress->Base.TargetTick; } set { ThrowIfInvalid(); TypedAddress->Base.TargetTick = value; } }
    public unsafe float SubtickStartFraction { get { ThrowIfInvalid(); return TypedAddress->Base.SubtickStartFraction; } set { ThrowIfInvalid(); TypedAddress->Base.SubtickStartFraction = value; } }
    public unsafe float SubtickEndFraction { get { ThrowIfInvalid(); return TypedAddress->Base.SubtickEndFraction; } set { ThrowIfInvalid(); TypedAddress->Base.SubtickEndFraction = value; } }

    public unsafe Vector OutWishVel { get { ThrowIfInvalid(); return TypedAddress->OutWishVel; } set { ThrowIfInvalid(); TypedAddress->OutWishVel = value; } }
    public unsafe QAngle OldAngles { get { ThrowIfInvalid(); return TypedAddress->OldAngles; } set { ThrowIfInvalid(); TypedAddress->OldAngles = value; } }
    public unsafe Vector2D WalkWishedVelocity { get { ThrowIfInvalid(); return TypedAddress->WalkWishedVelocity; } set { ThrowIfInvalid(); TypedAddress->WalkWishedVelocity = value; } }
    public unsafe Vector Acceleration { get { ThrowIfInvalid(); return TypedAddress->Acceleration; } set { ThrowIfInvalid(); TypedAddress->Acceleration = value; } }
    public unsafe Vector ContinuousAcceleration { get { ThrowIfInvalid(); return TypedAddress->ContinuousAcceleration; } set { ThrowIfInvalid(); TypedAddress->ContinuousAcceleration = value; } }
    public unsafe float MaxSpeed { get { ThrowIfInvalid(); return TypedAddress->MaxSpeed; } set { ThrowIfInvalid(); TypedAddress->MaxSpeed = value; } }
    public unsafe float ClientMaxSpeed { get { ThrowIfInvalid(); return TypedAddress->ClientMaxSpeed; } set { ThrowIfInvalid(); TypedAddress->ClientMaxSpeed = value; } }
    public unsafe float FrictionDecel { get { ThrowIfInvalid(); return TypedAddress->FrictionDecel; } set { ThrowIfInvalid(); TypedAddress->FrictionDecel = value; } }
    public unsafe bool InAir { get { ThrowIfInvalid(); return TypedAddress->InAir; } set { ThrowIfInvalid(); TypedAddress->InAir = value; } }
    public unsafe bool GameCodeMovedPlayer { get { ThrowIfInvalid(); return TypedAddress->GameCodeMovedPlayer; } set { ThrowIfInvalid(); TypedAddress->GameCodeMovedPlayer = value; } }
}
