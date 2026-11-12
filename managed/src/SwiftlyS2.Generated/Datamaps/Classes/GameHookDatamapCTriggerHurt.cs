using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerHurt(GameHooksService owner) : IGameHookDatamapCTriggerHurt
{
    internal readonly CTriggerHurtHurtThinkHook CTriggerHurtHurtThinkHook = new(owner);
    internal readonly CTriggerHurtNavThinkHook CTriggerHurtNavThinkHook = new(owner);
    internal readonly CTriggerHurtRadiationThinkHook CTriggerHurtRadiationThinkHook = new(owner);

    public ICTriggerHurtHurtThinkHook HurtThink => CTriggerHurtHurtThinkHook;
    public ICTriggerHurtNavThinkHook NavThink => CTriggerHurtNavThinkHook;
    public ICTriggerHurtRadiationThinkHook RadiationThink => CTriggerHurtRadiationThinkHook;

    internal void UnregisterListeners()
    {
        CTriggerHurtHurtThinkHook.UnregisterListeners();
        CTriggerHurtNavThinkHook.UnregisterListeners();
        CTriggerHurtRadiationThinkHook.UnregisterListeners();
    }
}