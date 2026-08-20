using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBaseAnimGraph : IGameHookDatamapCBaseAnimGraph
{
    internal readonly CBaseAnimGraphChoreoServicesThinkHook CBaseAnimGraphChoreoServicesThinkHook = new();

    public ICBaseAnimGraphChoreoServicesThinkHook ChoreoServicesThink => CBaseAnimGraphChoreoServicesThinkHook;

    internal void UnregisterListeners()
    {
        CBaseAnimGraphChoreoServicesThinkHook.UnregisterListeners();
    }
}