using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCEntityFlame : IGameHookDatamapCEntityFlame
{
    internal readonly CEntityFlameFlameThinkHook CEntityFlameFlameThinkHook = new();

    public ICEntityFlameFlameThinkHook FlameThink => CEntityFlameFlameThinkHook;

    internal void UnregisterListeners()
    {
        CEntityFlameFlameThinkHook.UnregisterListeners();
    }
}