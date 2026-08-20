namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCSprite
{
    public ICSpriteAnimateThinkHook AnimateThink { get; }
    public ICSpriteAnimateUntilDeadHook AnimateUntilDead { get; }
    public ICSpriteBeginFadeOutThinkHook BeginFadeOutThink { get; }
    public ICSpriteExpandThinkHook ExpandThink { get; }
}