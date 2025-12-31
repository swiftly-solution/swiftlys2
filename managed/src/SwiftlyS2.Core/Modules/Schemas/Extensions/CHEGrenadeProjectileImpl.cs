using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CHEGrenadeProjectileImpl : CHEGrenadeProjectile
{
    public static CHEGrenadeProjectile EmitGrenade( Vector pos, QAngle angle, Vector velocity, CBasePlayerPawn? owner )
    {
        NativeBinding.ThrowIfNonMainThread();
        return new CHEGrenadeProjectileImpl(GameFunctions.CHEGrenadeProjectile_EmitGrenade(pos, angle, velocity,
            owner?.Address ?? nint.Zero, (uint)HelpersService.WeaponItemDefinitionIndices["weapon_hegrenade"]));
    }

    public static Task<CHEGrenadeProjectile> EmitGrenadeAsync( Vector pos, QAngle angle, Vector velocity,
        CBasePlayerPawn? owner )
    {
        return SchedulerManager.QueueOrNow(() => EmitGrenade(pos, angle, velocity, owner));
    }
}