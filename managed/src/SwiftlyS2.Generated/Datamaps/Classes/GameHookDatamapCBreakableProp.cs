using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBreakableProp : IGameHookDatamapCBreakableProp
{
    internal readonly CBreakablePropBreakThinkHook CBreakablePropBreakThinkHook = new();
    internal readonly CBreakablePropRampToDefaultFadeScaleHook CBreakablePropRampToDefaultFadeScaleHook = new();

    public ICBreakablePropBreakThinkHook BreakThink => CBreakablePropBreakThinkHook;
    public ICBreakablePropRampToDefaultFadeScaleHook RampToDefaultFadeScale => CBreakablePropRampToDefaultFadeScaleHook;

    internal void UnregisterListeners()
    {
        CBreakablePropBreakThinkHook.UnregisterListeners();
        CBreakablePropRampToDefaultFadeScaleHook.UnregisterListeners();
    }
}