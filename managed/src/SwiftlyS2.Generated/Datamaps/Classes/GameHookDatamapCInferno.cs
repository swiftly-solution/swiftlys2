using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCInferno(GameHooksService owner) : IGameHookDatamapCInferno
{
    internal readonly CInfernoInfernoThinkHook CInfernoInfernoThinkHook = new(owner);

    public ICInfernoInfernoThinkHook InfernoThink => CInfernoInfernoThinkHook;

    internal void UnregisterListeners()
    {
        CInfernoInfernoThinkHook.UnregisterListeners();
    }
}