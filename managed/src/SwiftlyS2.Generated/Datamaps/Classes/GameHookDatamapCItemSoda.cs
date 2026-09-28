using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCItemSoda : IGameHookDatamapCItemSoda
{
    internal readonly CItemSodaCanThinkHook CItemSodaCanThinkHook = new();

    public ICItemSodaCanThinkHook CanThink => CItemSodaCanThinkHook;

    internal void UnregisterListeners()
    {
        CItemSodaCanThinkHook.UnregisterListeners();
    }
}