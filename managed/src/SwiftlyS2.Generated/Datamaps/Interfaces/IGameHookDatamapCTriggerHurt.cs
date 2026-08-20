namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCTriggerHurt
{
    public ICTriggerHurtHurtThinkHook HurtThink { get; }
    public ICTriggerHurtNavThinkHook NavThink { get; }
    public ICTriggerHurtRadiationThinkHook RadiationThink { get; }
}