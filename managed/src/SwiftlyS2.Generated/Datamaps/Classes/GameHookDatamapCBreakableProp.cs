using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBreakableProp(GameHooksService owner) : IGameHookDatamapCBreakableProp
{
    internal readonly CBreakablePropBreakThinkHook CBreakablePropBreakThinkHook = new(owner);
    internal readonly CBreakablePropRampToDefaultFadeScaleHook CBreakablePropRampToDefaultFadeScaleHook = new(owner);

    public ICBreakablePropBreakThinkHook BreakThink => CBreakablePropBreakThinkHook;
    public ICBreakablePropRampToDefaultFadeScaleHook RampToDefaultFadeScale => CBreakablePropRampToDefaultFadeScaleHook;

    internal void UnregisterListeners()
    {
        CBreakablePropBreakThinkHook.UnregisterListeners();
        CBreakablePropRampToDefaultFadeScaleHook.UnregisterListeners();
    }
}