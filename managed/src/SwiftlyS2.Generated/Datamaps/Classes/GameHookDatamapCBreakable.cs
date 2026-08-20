using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBreakable : IGameHookDatamapCBreakable
{
    internal readonly CBreakableDieHook CBreakableDieHook = new();

    public ICBreakableDieHook Die => CBreakableDieHook;

    internal void UnregisterListeners()
    {
        CBreakableDieHook.UnregisterListeners();
    }
}