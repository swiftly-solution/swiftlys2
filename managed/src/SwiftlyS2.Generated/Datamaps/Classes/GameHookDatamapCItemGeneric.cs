using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCItemGeneric(GameHooksService owner) : IGameHookDatamapCItemGeneric
{
    internal readonly CItemGenericItemGenericTouchHook CItemGenericItemGenericTouchHook = new(owner);

    public ICItemGenericItemGenericTouchHook ItemGenericTouch => CItemGenericItemGenericTouchHook;

    internal void UnregisterListeners()
    {
        CItemGenericItemGenericTouchHook.UnregisterListeners();
    }
}