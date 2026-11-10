using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCColorCorrection(GameHooksService owner) : IGameHookDatamapCColorCorrection
{
    internal readonly CColorCorrectionFadeInThinkHook CColorCorrectionFadeInThinkHook = new(owner);
    internal readonly CColorCorrectionFadeOutThinkHook CColorCorrectionFadeOutThinkHook = new(owner);

    public ICColorCorrectionFadeInThinkHook FadeInThink => CColorCorrectionFadeInThinkHook;
    public ICColorCorrectionFadeOutThinkHook FadeOutThink => CColorCorrectionFadeOutThinkHook;

    internal void UnregisterListeners()
    {
        CColorCorrectionFadeInThinkHook.UnregisterListeners();
        CColorCorrectionFadeOutThinkHook.UnregisterListeners();
    }
}