using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCItemDefuser : IGameHookDatamapCItemDefuser
{
    internal readonly CItemDefuserActivateThinkHook CItemDefuserActivateThinkHook = new();
    internal readonly CItemDefuserDefuserTouchHook CItemDefuserDefuserTouchHook = new();

    public ICItemDefuserActivateThinkHook ActivateThink => CItemDefuserActivateThinkHook;
    public ICItemDefuserDefuserTouchHook DefuserTouch => CItemDefuserDefuserTouchHook;

    internal void UnregisterListeners()
    {
        CItemDefuserActivateThinkHook.UnregisterListeners();
        CItemDefuserDefuserTouchHook.UnregisterListeners();
    }
}