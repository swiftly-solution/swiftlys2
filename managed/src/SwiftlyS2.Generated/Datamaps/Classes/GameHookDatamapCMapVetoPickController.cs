using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCMapVetoPickController : IGameHookDatamapCMapVetoPickController
{
    internal readonly CMapVetoPickControllerVoteControllerThinkHook CMapVetoPickControllerVoteControllerThinkHook = new();

    public ICMapVetoPickControllerVoteControllerThinkHook VoteControllerThink => CMapVetoPickControllerVoteControllerThinkHook;

    internal void UnregisterListeners()
    {
        CMapVetoPickControllerVoteControllerThinkHook.UnregisterListeners();
    }
}