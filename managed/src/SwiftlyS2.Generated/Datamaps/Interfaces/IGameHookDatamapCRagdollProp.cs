namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCRagdollProp
{
    public ICRagdollPropAttachedItemsThinkHook AttachedItemsThink { get; }
    public ICRagdollPropClearFlagsThinkHook ClearFlagsThink { get; }
    public ICRagdollPropFadeOutThinkHook FadeOutThink { get; }
    public ICRagdollPropSetDebrisThinkHook SetDebrisThink { get; }
    public ICRagdollPropSettleThinkHook SettleThink { get; }
}