using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCColorCorrection : IGameHookDatamapCColorCorrection
{
    internal readonly CColorCorrectionFadeInThinkHook CColorCorrectionFadeInThinkHook = new();
    internal readonly CColorCorrectionFadeOutThinkHook CColorCorrectionFadeOutThinkHook = new();

    public ICColorCorrectionFadeInThinkHook FadeInThink => CColorCorrectionFadeInThinkHook;
    public ICColorCorrectionFadeOutThinkHook FadeOutThink => CColorCorrectionFadeOutThinkHook;

    internal void UnregisterListeners()
    {
        CColorCorrectionFadeInThinkHook.UnregisterListeners();
        CColorCorrectionFadeOutThinkHook.UnregisterListeners();
    }
}