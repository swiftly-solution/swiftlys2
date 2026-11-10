using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCColorCorrectionVolume(GameHooksService owner) : IGameHookDatamapCColorCorrectionVolume
{
    internal readonly CColorCorrectionVolumeThinkFuncHook CColorCorrectionVolumeThinkFuncHook = new(owner);

    public ICColorCorrectionVolumeThinkFuncHook ThinkFunc => CColorCorrectionVolumeThinkFuncHook;

    internal void UnregisterListeners()
    {
        CColorCorrectionVolumeThinkFuncHook.UnregisterListeners();
    }
}