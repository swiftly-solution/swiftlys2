using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCLogicNPCCounter : IGameHookDatamapCLogicNPCCounter
{
    internal readonly CLogicNPCCounterSetNPCCounterThinkHook CLogicNPCCounterSetNPCCounterThinkHook = new();

    public ICLogicNPCCounterSetNPCCounterThinkHook SetNPCCounterThink => CLogicNPCCounterSetNPCCounterThinkHook;

    internal void UnregisterListeners()
    {
        CLogicNPCCounterSetNPCCounterThinkHook.UnregisterListeners();
    }
}