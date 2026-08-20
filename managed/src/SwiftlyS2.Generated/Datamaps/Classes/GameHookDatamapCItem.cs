using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCItem : IGameHookDatamapCItem
{
    internal readonly CItemComeToRestHook CItemComeToRestHook = new();
    internal readonly CItemItemTouchHook CItemItemTouchHook = new();
    internal readonly CItemMaterializeHook CItemMaterializeHook = new();

    public ICItemComeToRestHook ComeToRest => CItemComeToRestHook;
    public ICItemItemTouchHook ItemTouch => CItemItemTouchHook;
    public ICItemMaterializeHook Materialize => CItemMaterializeHook;

    internal void UnregisterListeners()
    {
        CItemComeToRestHook.UnregisterListeners();
        CItemItemTouchHook.UnregisterListeners();
        CItemMaterializeHook.UnregisterListeners();
    }
}