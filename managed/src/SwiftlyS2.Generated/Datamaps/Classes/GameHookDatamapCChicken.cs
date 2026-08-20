using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCChicken : IGameHookDatamapCChicken
{
    internal readonly CChickenChickenThinkHook CChickenChickenThinkHook = new();
    internal readonly CChickenChickenTouchHook CChickenChickenTouchHook = new();
    internal readonly CChickenChickenUseHook CChickenChickenUseHook = new();

    public ICChickenChickenThinkHook ChickenThink => CChickenChickenThinkHook;
    public ICChickenChickenTouchHook ChickenTouch => CChickenChickenTouchHook;
    public ICChickenChickenUseHook ChickenUse => CChickenChickenUseHook;

    internal void UnregisterListeners()
    {
        CChickenChickenThinkHook.UnregisterListeners();
        CChickenChickenTouchHook.UnregisterListeners();
        CChickenChickenUseHook.UnregisterListeners();
    }
}