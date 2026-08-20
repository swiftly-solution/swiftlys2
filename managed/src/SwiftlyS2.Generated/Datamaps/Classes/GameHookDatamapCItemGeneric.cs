using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCItemGeneric : IGameHookDatamapCItemGeneric
{
    internal readonly CItemGenericItemGenericTouchHook CItemGenericItemGenericTouchHook = new();

    public ICItemGenericItemGenericTouchHook ItemGenericTouch => CItemGenericItemGenericTouchHook;

    internal void UnregisterListeners()
    {
        CItemGenericItemGenericTouchHook.UnregisterListeners();
    }
}