using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerHurt : IGameHookDatamapCTriggerHurt
{
    internal readonly CTriggerHurtHurtThinkHook CTriggerHurtHurtThinkHook = new();
    internal readonly CTriggerHurtNavThinkHook CTriggerHurtNavThinkHook = new();
    internal readonly CTriggerHurtRadiationThinkHook CTriggerHurtRadiationThinkHook = new();

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