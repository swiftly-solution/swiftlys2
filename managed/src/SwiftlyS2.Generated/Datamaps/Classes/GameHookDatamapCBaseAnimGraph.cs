using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBaseAnimGraph(GameHooksService owner) : IGameHookDatamapCBaseAnimGraph
{
    internal readonly CBaseAnimGraphChoreoServicesThinkHook CBaseAnimGraphChoreoServicesThinkHook = new(owner);

    public ICBaseAnimGraphChoreoServicesThinkHook ChoreoServicesThink => CBaseAnimGraphChoreoServicesThinkHook;

    internal void UnregisterListeners()
    {
        CBaseAnimGraphChoreoServicesThinkHook.UnregisterListeners();
    }
}