namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCTriggerLerpObject
{
    public ICTriggerLerpObjectAttachedEntityThinkHook AttachedEntityThink { get; }
    public ICTriggerLerpObjectLerpThinkHook LerpThink { get; }
    public ICTriggerLerpObjectUnsetWaitForEntityHook UnsetWaitForEntity { get; }
}