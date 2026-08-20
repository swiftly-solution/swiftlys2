namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCItem
{
    public ICItemComeToRestHook ComeToRest { get; }
    public ICItemItemTouchHook ItemTouch { get; }
    public ICItemMaterializeHook Materialize { get; }
}