using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCItemDefuser(GameHooksService owner) : IGameHookDatamapCItemDefuser
{
    internal readonly CItemDefuserActivateThinkHook CItemDefuserActivateThinkHook = new(owner);
    internal readonly CItemDefuserDefuserTouchHook CItemDefuserDefuserTouchHook = new(owner);

    public ICItemDefuserActivateThinkHook ActivateThink => CItemDefuserActivateThinkHook;
    public ICItemDefuserDefuserTouchHook DefuserTouch => CItemDefuserDefuserTouchHook;

    internal void UnregisterListeners()
    {
        CItemDefuserActivateThinkHook.UnregisterListeners();
        CItemDefuserDefuserTouchHook.UnregisterListeners();
    }
}