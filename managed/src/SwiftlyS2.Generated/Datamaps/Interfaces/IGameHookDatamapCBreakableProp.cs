namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCBreakableProp
{
    public ICBreakablePropBreakThinkHook BreakThink { get; }
    public ICBreakablePropRampToDefaultFadeScaleHook RampToDefaultFadeScale { get; }
}