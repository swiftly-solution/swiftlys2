using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCEntityFlame(GameHooksService owner) : IGameHookDatamapCEntityFlame
{
    internal readonly CEntityFlameFlameThinkHook CEntityFlameFlameThinkHook = new(owner);

    public ICEntityFlameFlameThinkHook FlameThink => CEntityFlameFlameThinkHook;

    internal void UnregisterListeners()
    {
        CEntityFlameFlameThinkHook.UnregisterListeners();
    }
}