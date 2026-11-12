using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCItemSoda(GameHooksService owner) : IGameHookDatamapCItemSoda
{
    internal readonly CItemSodaCanThinkHook CItemSodaCanThinkHook = new(owner);

    public ICItemSodaCanThinkHook CanThink => CItemSodaCanThinkHook;

    internal void UnregisterListeners()
    {
        CItemSodaCanThinkHook.UnregisterListeners();
    }
}