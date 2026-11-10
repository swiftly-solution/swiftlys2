using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCVoteController(GameHooksService owner) : IGameHookDatamapCVoteController
{
    internal readonly CVoteControllerVoteControllerThinkHook CVoteControllerVoteControllerThinkHook = new(owner);

    public ICVoteControllerVoteControllerThinkHook VoteControllerThink => CVoteControllerVoteControllerThinkHook;

    internal void UnregisterListeners()
    {
        CVoteControllerVoteControllerThinkHook.UnregisterListeners();
    }
}