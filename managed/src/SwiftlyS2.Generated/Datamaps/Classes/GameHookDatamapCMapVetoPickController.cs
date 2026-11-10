using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCMapVetoPickController(GameHooksService owner) : IGameHookDatamapCMapVetoPickController
{
    internal readonly CMapVetoPickControllerVoteControllerThinkHook CMapVetoPickControllerVoteControllerThinkHook = new(owner);

    public ICMapVetoPickControllerVoteControllerThinkHook VoteControllerThink => CMapVetoPickControllerVoteControllerThinkHook;

    internal void UnregisterListeners()
    {
        CMapVetoPickControllerVoteControllerThinkHook.UnregisterListeners();
    }
}