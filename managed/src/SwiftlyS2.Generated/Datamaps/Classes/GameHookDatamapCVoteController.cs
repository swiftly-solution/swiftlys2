using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCVoteController : IGameHookDatamapCVoteController
{
    internal readonly CVoteControllerVoteControllerThinkHook CVoteControllerVoteControllerThinkHook = new();

    public ICVoteControllerVoteControllerThinkHook VoteControllerThink => CVoteControllerVoteControllerThinkHook;

    internal void UnregisterListeners()
    {
        CVoteControllerVoteControllerThinkHook.UnregisterListeners();
    }
}