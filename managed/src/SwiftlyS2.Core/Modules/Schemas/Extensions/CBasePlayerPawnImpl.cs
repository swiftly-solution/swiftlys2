using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CBasePlayerPawnImpl : CBasePlayerPawn
{
    public Vector? EyePosition {
        get {
            if (!IsValid)
            {
                return null;
            }

            if (AbsOrigin == null || ViewOffset == null)
            {
                return null;
            }

            var absOrigin = AbsOrigin.Value;
            absOrigin.Z += ViewOffset.Z.Value;
            return absOrigin;
        }
    }

    public float GroundDistance {
        get {
            if (!IsValid || AbsOrigin == null)
            {
                return -1f;
            }

            var start = AbsOrigin.Value;
            var angle = new QAngle(90f, 0f, 0f);
            angle.ToDirectionVectors(out var fwd, out var _, out var _);
            var end = start + new Vector(
                fwd.X * 8192f,
                fwd.Y * 8192f,
                fwd.Z * 8192f
            );

            var trace = new CGameTrace();
            TraceManager.SimpleTrace(start, end, RayType_t.RAY_TYPE_HULL, RnQueryObjectSet.All, MaskTrace.Sky, MaskTrace.Empty, MaskTrace.Empty, CollisionGroup.Always, ref trace, Address, nint.Zero);
            return trace.Distance;
        }
    }

    public MaskTrace InteractsWith {
        get {
            return !IsValid ? MaskTrace.Empty : (MaskTrace)Collision.CollisionAttribute.InteractsWith;
        }
    }

    public MaskTrace InteractsAs {
        get {
            return !IsValid ? MaskTrace.Empty : (MaskTrace)Collision.CollisionAttribute.InteractsAs;
        }
    }

    public MaskTrace InteractsExclude {
        get {
            return !IsValid ? MaskTrace.Empty : (MaskTrace)Collision.CollisionAttribute.InteractsExclude;
        }
    }

    public void CommitSuicide( bool explode, bool force )
    {
        GameFunctions.PawnCommitSuicide(Address, explode, force);
    }

    public bool HasLineOfSight( CCSPlayerPawn targetPlayer, float? fieldOfViewDegrees = null )
    {
        if (!IsValid || !targetPlayer.IsValid || this.LifeState != (byte)LifeState_t.LIFE_ALIVE || targetPlayer.LifeState != (byte)LifeState_t.LIFE_ALIVE)
        {
            return false;
        }

        var playerPawn = new CCSPlayerPawnImpl(this.Address);
        if (!(playerPawn.OriginalController.Value?.IsValid ?? false))
        {
            return false;
        }

        var trace = new CGameTrace();
        TraceManager.SimpleTrace(this.EyePosition!.Value, targetPlayer.EyePosition!.Value, RayType_t.RAY_TYPE_HULL, RnQueryObjectSet.All, MaskTrace.Player, MaskTrace.Trigger, MaskTrace.Empty, CollisionGroup.Always, ref trace, Address, nint.Zero);
        if (!trace.HitPlayer(out var player) || player?.PlayerPawn?.Index != targetPlayer.Index)
        {
            return false;
        }

        var desiredFov = playerPawn.OriginalController.Value.DesiredFOV;
        var halfFov = fieldOfViewDegrees ?? (desiredFov <= 0f ? 52f : desiredFov / 2f);

        // Calculate the angle between the player's view direction and the direction to the target
        playerPawn.EyeAngles.ToDirectionVectors(out var playerForward, out var _, out var _);
        var directionToTarget = targetPlayer.EyePosition!.Value - this.EyePosition!.Value;
        directionToTarget.Normalize();

        // Calculate the angle using the dot product to avoid coordinate system issues
        var dotProduct = Math.Clamp(playerForward.Dot(directionToTarget), -1f, 1f);
        var angleInDegrees = Math.Acos(dotProduct) * (180f / Math.PI);

        return angleInDegrees <= halfFov;
    }
}