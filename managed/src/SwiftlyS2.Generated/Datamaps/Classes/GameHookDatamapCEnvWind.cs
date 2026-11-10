using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCEnvWind(GameHooksService owner) : IGameHookDatamapCEnvWind
{
    internal readonly CEnvWindWindThinkHook CEnvWindWindThinkHook = new(owner);

    public ICEnvWindWindThinkHook WindThink => CEnvWindWindThinkHook;

    internal void UnregisterListeners()
    {
        CEnvWindWindThinkHook.UnregisterListeners();
    }
}