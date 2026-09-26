using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCItemGenericTriggerHelper : IGameHookDatamapCItemGenericTriggerHelper
{
    internal readonly CItemGenericTriggerHelperItemGenericTriggerHelperTouchHook CItemGenericTriggerHelperItemGenericTriggerHelperTouchHook = new();

    public ICItemGenericTriggerHelperItemGenericTriggerHelperTouchHook ItemGenericTriggerHelperTouch => CItemGenericTriggerHelperItemGenericTriggerHelperTouchHook;

    internal void UnregisterListeners()
    {
        CItemGenericTriggerHelperItemGenericTriggerHelperTouchHook.UnregisterListeners();
    }
}