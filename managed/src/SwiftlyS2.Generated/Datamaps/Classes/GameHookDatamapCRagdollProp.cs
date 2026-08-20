using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCRagdollProp : IGameHookDatamapCRagdollProp
{
    internal readonly CRagdollPropAttachedItemsThinkHook CRagdollPropAttachedItemsThinkHook = new();
    internal readonly CRagdollPropClearFlagsThinkHook CRagdollPropClearFlagsThinkHook = new();
    internal readonly CRagdollPropFadeOutThinkHook CRagdollPropFadeOutThinkHook = new();
    internal readonly CRagdollPropSetDebrisThinkHook CRagdollPropSetDebrisThinkHook = new();
    internal readonly CRagdollPropSettleThinkHook CRagdollPropSettleThinkHook = new();

    public ICRagdollPropAttachedItemsThinkHook AttachedItemsThink => CRagdollPropAttachedItemsThinkHook;
    public ICRagdollPropClearFlagsThinkHook ClearFlagsThink => CRagdollPropClearFlagsThinkHook;
    public ICRagdollPropFadeOutThinkHook FadeOutThink => CRagdollPropFadeOutThinkHook;
    public ICRagdollPropSetDebrisThinkHook SetDebrisThink => CRagdollPropSetDebrisThinkHook;
    public ICRagdollPropSettleThinkHook SettleThink => CRagdollPropSettleThinkHook;

    internal void UnregisterListeners()
    {
        CRagdollPropAttachedItemsThinkHook.UnregisterListeners();
        CRagdollPropClearFlagsThinkHook.UnregisterListeners();
        CRagdollPropFadeOutThinkHook.UnregisterListeners();
        CRagdollPropSetDebrisThinkHook.UnregisterListeners();
        CRagdollPropSettleThinkHook.UnregisterListeners();
    }
}