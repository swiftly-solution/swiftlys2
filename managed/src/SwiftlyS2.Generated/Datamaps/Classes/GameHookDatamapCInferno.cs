using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCInferno : IGameHookDatamapCInferno
{
    internal readonly CInfernoInfernoThinkHook CInfernoInfernoThinkHook = new();

    public ICInfernoInfernoThinkHook InfernoThink => CInfernoInfernoThinkHook;

    internal void UnregisterListeners()
    {
        CInfernoInfernoThinkHook.UnregisterListeners();
    }
}