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

    public unsafe bool HasZeroFrametime { get { ThrowIfInvalid(); return TypedAddress->Base.HasZeroFrametime; } }
    public unsafe bool IsLateCommand { get { ThrowIfInvalid(); return TypedAddress->Base.IsLateCommand; } }

    public unsafe CHandle<CCSPlayerPawn> PlayerHandle { get { ThrowIfInvalid(); return TypedAddress->Base.PlayerHandle; } }
    public unsafe QAngle AbsViewAngles { get { ThrowIfInvalid(); return TypedAddress->Base.AbsViewAngles; } }
    public unsafe QAngle ViewAngles { get { ThrowIfInvalid(); return TypedAddress->Base.ViewAngles; } }
    public unsafe Vector LastMovementImpulses { get { ThrowIfInvalid(); return TypedAddress->Base.LastMovementImpulses; } }
    public unsafe float ForwardMove { get { ThrowIfInvalid(); return TypedAddress->Base.ForwardMove; } }
    // Warning! Flipped compared to CS:GO, moving right gives negative value
    public unsafe float SideMove { get { ThrowIfInvalid(); return TypedAddress->Base.SideMove; } }
    public unsafe float UpMove { get { ThrowIfInvalid(); return TypedAddress->Base.UpMove; } }
    public unsafe Vector Velocity { get { ThrowIfInvalid(); return TypedAddress->Base.Velocity; } }
    public unsafe QAngle Angles { get { ThrowIfInvalid(); return TypedAddress->Base.Angles; } }
    public unsafe CUtlVector<SubtickMove> SubtickMoves { get { ThrowIfInvalid(); return TypedAddress->Base.SubtickMoves; } }
    public unsafe CUtlVector<SubtickMove> AttackSubtickMoves { get { ThrowIfInvalid(); return TypedAddress->Base.AttackSubtickMoves; } }
    public unsafe bool HasSubtickInputs { get { ThrowIfInvalid(); return TypedAddress->Base.HasSubtickInputs; } }
    public unsafe CUtlVector<TouchListT> TouchList { get { ThrowIfInvalid(); return TypedAddress->Base.TouchList; } }
    public unsafe Vector CollisionNormal { get { ThrowIfInvalid(); return TypedAddress->Base.CollisionNormal; } }
    public unsafe Vector GroundNormal { get { ThrowIfInvalid(); return TypedAddress->Base.GroundNormal; } }
    public unsafe Vector AbsOrigin { get { ThrowIfInvalid(); return TypedAddress->Base.AbsOrigin; } }
    public unsafe int TickCount { get { ThrowIfInvalid(); return TypedAddress->Base.TickCount; } }
    public unsafe int TargetTick { get { ThrowIfInvalid(); return TypedAddress->Base.TargetTick; } }
    public unsafe float SubtickStartFraction { get { ThrowIfInvalid(); return TypedAddress->Base.SubtickStartFraction; } }
    public unsafe float SubtickEndFraction { get { ThrowIfInvalid(); return TypedAddress->Base.SubtickEndFraction; } }

    public unsafe Vector OutWishVel { get { ThrowIfInvalid(); return TypedAddress->OutWishVel; } }
    public unsafe QAngle OldAngles { get { ThrowIfInvalid(); return TypedAddress->OldAngles; } }
    public unsafe Vector2D WalkWishedVelocity { get { ThrowIfInvalid(); return TypedAddress->WalkWishedVelocity; } }
    public unsafe Vector Acceleration { get { ThrowIfInvalid(); return TypedAddress->Acceleration; } }
    public unsafe Vector ContinuousAcceleration { get { ThrowIfInvalid(); return TypedAddress->ContinuousAcceleration; } }
    public unsafe float MaxSpeed { get { ThrowIfInvalid(); return TypedAddress->MaxSpeed; } }
    public unsafe float ClientMaxSpeed { get { ThrowIfInvalid(); return TypedAddress->ClientMaxSpeed; } }
    public unsafe float FrictionDecel { get { ThrowIfInvalid(); return TypedAddress->FrictionDecel; } }
    public unsafe bool InAir { get { ThrowIfInvalid(); return TypedAddress->InAir; } }
    public unsafe bool GameCodeMovedPlayer { get { ThrowIfInvalid(); return TypedAddress->GameCodeMovedPlayer; } }
}
