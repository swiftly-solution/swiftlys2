using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCEnvWind : IGameHookDatamapCEnvWind
{
    internal readonly CEnvWindWindThinkHook CEnvWindWindThinkHook = new();

    public ICEnvWindWindThinkHook WindThink => CEnvWindWindThinkHook;

    internal void UnregisterListeners()
    {
        CEnvWindWindThinkHook.UnregisterListeners();
    }
}