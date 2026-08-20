using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCColorCorrectionVolume : IGameHookDatamapCColorCorrectionVolume
{
    internal readonly CColorCorrectionVolumeThinkFuncHook CColorCorrectionVolumeThinkFuncHook = new();

    public ICColorCorrectionVolumeThinkFuncHook ThinkFunc => CColorCorrectionVolumeThinkFuncHook;

    internal void UnregisterListeners()
    {
        CColorCorrectionVolumeThinkFuncHook.UnregisterListeners();
    }
}