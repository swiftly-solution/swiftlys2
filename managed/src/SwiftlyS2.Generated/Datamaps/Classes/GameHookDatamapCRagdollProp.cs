using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCRagdollProp(GameHooksService owner) : IGameHookDatamapCRagdollProp
{
    internal readonly CRagdollPropAttachedItemsThinkHook CRagdollPropAttachedItemsThinkHook = new(owner);
    internal readonly CRagdollPropClearFlagsThinkHook CRagdollPropClearFlagsThinkHook = new(owner);
    internal readonly CRagdollPropFadeOutThinkHook CRagdollPropFadeOutThinkHook = new(owner);
    internal readonly CRagdollPropSetDebrisThinkHook CRagdollPropSetDebrisThinkHook = new(owner);
    internal readonly CRagdollPropSettleThinkHook CRagdollPropSettleThinkHook = new(owner);

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