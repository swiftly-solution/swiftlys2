using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerLerpObject(GameHooksService owner) : IGameHookDatamapCTriggerLerpObject
{
    internal readonly CTriggerLerpObjectAttachedEntityThinkHook CTriggerLerpObjectAttachedEntityThinkHook = new(owner);
    internal readonly CTriggerLerpObjectLerpThinkHook CTriggerLerpObjectLerpThinkHook = new(owner);
    internal readonly CTriggerLerpObjectUnsetWaitForEntityHook CTriggerLerpObjectUnsetWaitForEntityHook = new(owner);

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