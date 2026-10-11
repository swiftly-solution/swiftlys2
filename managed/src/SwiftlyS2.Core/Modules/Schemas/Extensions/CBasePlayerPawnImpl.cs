using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.Trace;

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

            var trace = TraceParams.Builder().WithLineRay(Vector.Zero).WithObjectQuery(RnQueryObjectSet.All)
                            .WithInteraction(MaskTrace.Sky, MaskTrace.Empty, MaskTrace.Empty).WithCollisionGroup(CollisionGroup.Always)
                            .IgnoreEntity(this).Build();
            var result = TraceManager.TraceShape(start, end, trace);
            return result.Distance;
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

        if (this is not CCSPlayerPawn playerPawn) return false;
        var originalControllerHandle = playerPawn.OriginalController;
        if (!originalControllerHandle.IsValid) return false;
        var originalController = originalControllerHandle.Value!;
        if(!originalController.IsValid) return false;

        var trace = TraceParams.Builder().WithHullRay(Vector.Zero, Vector.Zero).WithObjectQuery(RnQueryObjectSet.All)
                        .WithInteraction(MaskTrace.Player, MaskTrace.Trigger, MaskTrace.Empty).WithCollisionGroup(CollisionGroup.Always)
                        .IgnoreEntity(this).Build();
        var result = TraceManager.TraceShape(this.EyePosition!.Value, targetPlayer.EyePosition!.Value, trace);
        if (!result.HitPlayer(out var player) || player?.PlayerPawn?.Index != targetPlayer.Index) return false;

        var desiredFov = originalController.DesiredFOV;
        var halfFov = fieldOfViewDegrees ?? (desiredFov <= 0f ? 52f : desiredFov / 2f);

        playerPawn.EyeAngles.ToDirectionVectors(out var playerForward, out var _, out var _);
        var directionToTarget = targetPlayer.EyePosition!.Value - this.EyePosition!.Value;
        directionToTarget.Normalize();

        var dotProduct = Math.Clamp(playerForward.Dot(directionToTarget), -1f, 1f);
        var angleInDegrees = Math.Acos(dotProduct) * (180f / Math.PI);

        return angleInDegrees <= halfFov;
    }

    public IPlayer? ToPlayer()
    {
        return !IsValid ? null : (Controller.Value?.ToPlayer());
    }
}