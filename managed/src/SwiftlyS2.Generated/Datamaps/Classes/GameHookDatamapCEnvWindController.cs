using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCEnvWindController : IGameHookDatamapCEnvWindController
{
    internal readonly CEnvWindControllerWindThinkHook CEnvWindControllerWindThinkHook = new();

    public ICEnvWindControllerWindThinkHook WindThink => CEnvWindControllerWindThinkHook;

    internal void UnregisterListeners()
    {
        CEnvWindControllerWindThinkHook.UnregisterListeners();
    }
}