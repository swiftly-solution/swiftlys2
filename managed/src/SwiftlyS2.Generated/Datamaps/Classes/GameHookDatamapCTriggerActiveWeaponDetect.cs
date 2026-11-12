using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerActiveWeaponDetect(GameHooksService owner) : IGameHookDatamapCTriggerActiveWeaponDetect
{
    internal readonly CTriggerActiveWeaponDetectActiveWeaponThinkHook CTriggerActiveWeaponDetectActiveWeaponThinkHook = new(owner);

    public ICTriggerActiveWeaponDetectActiveWeaponThinkHook ActiveWeaponThink => CTriggerActiveWeaponDetectActiveWeaponThinkHook;

    internal void UnregisterListeners()
    {
        CTriggerActiveWeaponDetectActiveWeaponThinkHook.UnregisterListeners();
    }
}