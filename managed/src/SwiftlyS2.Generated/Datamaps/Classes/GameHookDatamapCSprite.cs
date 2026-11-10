using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSprite(GameHooksService owner) : IGameHookDatamapCSprite
{
    internal readonly CSpriteAnimateThinkHook CSpriteAnimateThinkHook = new(owner);
    internal readonly CSpriteAnimateUntilDeadHook CSpriteAnimateUntilDeadHook = new(owner);
    internal readonly CSpriteBeginFadeOutThinkHook CSpriteBeginFadeOutThinkHook = new(owner);
    internal readonly CSpriteExpandThinkHook CSpriteExpandThinkHook = new(owner);

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