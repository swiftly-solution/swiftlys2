using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCEnvLaser(GameHooksService owner) : IGameHookDatamapCEnvLaser
{
    internal readonly CEnvLaserStrikeThinkHook CEnvLaserStrikeThinkHook = new(owner);

    public ICEnvLaserStrikeThinkHook StrikeThink => CEnvLaserStrikeThinkHook;

    internal void UnregisterListeners()
    {
        CEnvLaserStrikeThinkHook.UnregisterListeners();
    }
}