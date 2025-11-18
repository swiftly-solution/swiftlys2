using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "vote_cast_no"
/// </summary>
public interface EventVoteCastNo : IGameEvent<EventVoteCastNo> {

  static EventVoteCastNo IGameEvent<EventVoteCastNo>.Create(nint address) => new EventVoteCastNoImpl(address);

  static string IGameEvent<EventVoteCastNo>.GetName() => "vote_cast_no";

  static uint IGameEvent<EventVoteCastNo>.GetHash() => 0x73639B1Du;
  /// <summary>
  /// type: byte
  /// </summary>
  byte Team { get; set; }

  /// <summary>
  /// entity id of the voter
  /// <br/>
  /// type: long
  /// </summary>
  int EntityID { get; set; }

}
