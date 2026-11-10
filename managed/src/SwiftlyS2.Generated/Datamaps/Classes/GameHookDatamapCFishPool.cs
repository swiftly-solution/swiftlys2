using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFishPool(GameHooksService owner) : IGameHookDatamapCFishPool
{
    internal readonly CFishPoolUpdateHook CFishPoolUpdateHook = new(owner);

    public ICFishPoolUpdateHook Update => CFishPoolUpdateHook;

    internal void UnregisterListeners()
    {
        CFishPoolUpdateHook.UnregisterListeners();
    }
}