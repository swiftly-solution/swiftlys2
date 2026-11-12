using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBreakable(GameHooksService owner) : IGameHookDatamapCBreakable
{
    internal readonly CBreakableDieHook CBreakableDieHook = new(owner);

    public ICBreakableDieHook Die => CBreakableDieHook;

    internal void UnregisterListeners()
    {
        CBreakableDieHook.UnregisterListeners();
    }
}