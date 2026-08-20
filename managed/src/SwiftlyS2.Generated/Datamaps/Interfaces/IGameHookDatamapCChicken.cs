namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCChicken
{
    public ICChickenChickenThinkHook ChickenThink { get; }
    public ICChickenChickenTouchHook ChickenTouch { get; }
    public ICChickenChickenUseHook ChickenUse { get; }
}