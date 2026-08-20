using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSprite : IGameHookDatamapCSprite
{
    internal readonly CSpriteAnimateThinkHook CSpriteAnimateThinkHook = new();
    internal readonly CSpriteAnimateUntilDeadHook CSpriteAnimateUntilDeadHook = new();
    internal readonly CSpriteBeginFadeOutThinkHook CSpriteBeginFadeOutThinkHook = new();
    internal readonly CSpriteExpandThinkHook CSpriteExpandThinkHook = new();

    public ICSpriteAnimateThinkHook AnimateThink => CSpriteAnimateThinkHook;
    public ICSpriteAnimateUntilDeadHook AnimateUntilDead => CSpriteAnimateUntilDeadHook;
    public ICSpriteBeginFadeOutThinkHook BeginFadeOutThink => CSpriteBeginFadeOutThinkHook;
    public ICSpriteExpandThinkHook ExpandThink => CSpriteExpandThinkHook;

    internal void UnregisterListeners()
    {
        CSpriteAnimateThinkHook.UnregisterListeners();
        CSpriteAnimateUntilDeadHook.UnregisterListeners();
        CSpriteBeginFadeOutThinkHook.UnregisterListeners();
        CSpriteExpandThinkHook.UnregisterListeners();
    }
}