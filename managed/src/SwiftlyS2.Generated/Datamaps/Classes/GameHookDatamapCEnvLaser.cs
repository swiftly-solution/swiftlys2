using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCEnvLaser : IGameHookDatamapCEnvLaser
{
    internal readonly CEnvLaserStrikeThinkHook CEnvLaserStrikeThinkHook = new();

    public ICEnvLaserStrikeThinkHook StrikeThink => CEnvLaserStrikeThinkHook;

    internal void UnregisterListeners()
    {
        CEnvLaserStrikeThinkHook.UnregisterListeners();
    }
}