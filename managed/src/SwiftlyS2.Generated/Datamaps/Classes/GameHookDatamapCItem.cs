using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCItem(GameHooksService owner) : IGameHookDatamapCItem
{
    internal readonly CItemComeToRestHook CItemComeToRestHook = new(owner);
    internal readonly CItemItemTouchHook CItemItemTouchHook = new(owner);
    internal readonly CItemMaterializeHook CItemMaterializeHook = new(owner);

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