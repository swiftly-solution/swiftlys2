using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.Events;

internal class OnFindTargettedPlayersEvent : IOnFindTargettedPlayersEvent
{
    public required IPlayer Player { get; init; }
    public required string Target { get; init; }
    public required TargetSearchMode SearchMode { get; init; }
    public required IPlayer Candidate { get; init; }
    public bool IsMatch { get; set; }
    public HookResult Result { get; set; }
}
