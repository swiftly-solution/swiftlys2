using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFishPool : IGameHookDatamapCFishPool
{
    internal readonly CFishPoolUpdateHook CFishPoolUpdateHook = new();

    public ICFishPoolUpdateHook Update => CFishPoolUpdateHook;

    internal void UnregisterListeners()
    {
        CFishPoolUpdateHook.UnregisterListeners();
    }
}