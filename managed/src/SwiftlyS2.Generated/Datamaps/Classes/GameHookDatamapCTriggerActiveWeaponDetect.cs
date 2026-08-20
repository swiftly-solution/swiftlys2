using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerActiveWeaponDetect : IGameHookDatamapCTriggerActiveWeaponDetect
{
    internal readonly CTriggerActiveWeaponDetectActiveWeaponThinkHook CTriggerActiveWeaponDetectActiveWeaponThinkHook = new();

    public ICTriggerActiveWeaponDetectActiveWeaponThinkHook ActiveWeaponThink => CTriggerActiveWeaponDetectActiveWeaponThinkHook;

    internal void UnregisterListeners()
    {
        CTriggerActiveWeaponDetectActiveWeaponThinkHook.UnregisterListeners();
    }
}