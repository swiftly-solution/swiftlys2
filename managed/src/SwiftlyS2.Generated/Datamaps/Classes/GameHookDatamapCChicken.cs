using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCChicken(GameHooksService owner) : IGameHookDatamapCChicken
{
    internal readonly CChickenChickenThinkHook CChickenChickenThinkHook = new(owner);
    internal readonly CChickenChickenTouchHook CChickenChickenTouchHook = new(owner);
    internal readonly CChickenChickenUseHook CChickenChickenUseHook = new(owner);

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