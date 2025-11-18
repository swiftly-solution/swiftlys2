using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "vote_ended"
/// </summary>
public interface EventVoteEnded : IGameEvent<EventVoteEnded> {

  static EventVoteEnded IGameEvent<EventVoteEnded>.Create(nint address) => new EventVoteEndedImpl(address);

  static string IGameEvent<EventVoteEnded>.GetName() => "vote_ended";

  static uint IGameEvent<EventVoteEnded>.GetHash() => 0xBE602B9Au;
}
