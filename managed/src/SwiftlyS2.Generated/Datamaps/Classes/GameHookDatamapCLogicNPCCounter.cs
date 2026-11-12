using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCLogicNPCCounter(GameHooksService owner) : IGameHookDatamapCLogicNPCCounter
{
    internal readonly CLogicNPCCounterSetNPCCounterThinkHook CLogicNPCCounterSetNPCCounterThinkHook = new(owner);

    public ICLogicNPCCounterSetNPCCounterThinkHook SetNPCCounterThink => CLogicNPCCounterSetNPCCounterThinkHook;

    internal void UnregisterListeners()
    {
        CLogicNPCCounterSetNPCCounterThinkHook.UnregisterListeners();
    }
}