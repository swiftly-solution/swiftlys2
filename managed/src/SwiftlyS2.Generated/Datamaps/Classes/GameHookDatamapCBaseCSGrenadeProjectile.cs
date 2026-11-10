using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBaseCSGrenadeProjectile(GameHooksService owner) : IGameHookDatamapCBaseCSGrenadeProjectile
{
    internal readonly CBaseCSGrenadeProjectileDangerSoundThinkHook CBaseCSGrenadeProjectileDangerSoundThinkHook = new(owner);

    public ICBaseCSGrenadeProjectileDangerSoundThinkHook DangerSoundThink => CBaseCSGrenadeProjectileDangerSoundThinkHook;

    internal void UnregisterListeners()
    {
        CBaseCSGrenadeProjectileDangerSoundThinkHook.UnregisterListeners();
    }
}