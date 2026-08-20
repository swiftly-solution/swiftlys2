using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerLerpObject : IGameHookDatamapCTriggerLerpObject
{
    internal readonly CTriggerLerpObjectAttachedEntityThinkHook CTriggerLerpObjectAttachedEntityThinkHook = new();
    internal readonly CTriggerLerpObjectLerpThinkHook CTriggerLerpObjectLerpThinkHook = new();
    internal readonly CTriggerLerpObjectUnsetWaitForEntityHook CTriggerLerpObjectUnsetWaitForEntityHook = new();

    public ICTriggerLerpObjectAttachedEntityThinkHook AttachedEntityThink => CTriggerLerpObjectAttachedEntityThinkHook;
    public ICTriggerLerpObjectLerpThinkHook LerpThink => CTriggerLerpObjectLerpThinkHook;
    public ICTriggerLerpObjectUnsetWaitForEntityHook UnsetWaitForEntity => CTriggerLerpObjectUnsetWaitForEntityHook;

    internal void UnregisterListeners()
    {
        CTriggerLerpObjectAttachedEntityThinkHook.UnregisterListeners();
        CTriggerLerpObjectLerpThinkHook.UnregisterListeners();
        CTriggerLerpObjectUnsetWaitForEntityHook.UnregisterListeners();
    }
}