namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCColorCorrection
{
    public ICColorCorrectionFadeInThinkHook FadeInThink { get; }
    public ICColorCorrectionFadeOutThinkHook FadeOutThink { get; }
}