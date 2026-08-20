using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBaseCSGrenadeProjectile : IGameHookDatamapCBaseCSGrenadeProjectile
{
    internal readonly CBaseCSGrenadeProjectileDangerSoundThinkHook CBaseCSGrenadeProjectileDangerSoundThinkHook = new();

    public ICBaseCSGrenadeProjectileDangerSoundThinkHook DangerSoundThink => CBaseCSGrenadeProjectileDangerSoundThinkHook;

    internal void UnregisterListeners()
    {
        CBaseCSGrenadeProjectileDangerSoundThinkHook.UnregisterListeners();
    }
}