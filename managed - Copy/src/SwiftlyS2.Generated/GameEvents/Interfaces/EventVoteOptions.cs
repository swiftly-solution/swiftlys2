using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "vote_options"
/// </summary>
public interface EventVoteOptions : IGameEvent<EventVoteOptions> {

  static EventVoteOptions IGameEvent<EventVoteOptions>.Create(nint address) => new EventVoteOptionsImpl(address);

  static string IGameEvent<EventVoteOptions>.GetName() => "vote_options";

  static uint IGameEvent<EventVoteOptions>.GetHash() => 0x73B095B0u;
  /// <summary>
  /// Number of options - up to MAX_VOTE_OPTIONS
  /// <br/>
  /// type: byte
  /// </summary>
  byte Count { get; set; }

  /// <summary>
  /// type: string
  /// </summary>
  string Option1 { get; set; }

  /// <summary>
  /// type: string
  /// </summary>
  string Option2 { get; set; }

  /// <summary>
  /// type: string
  /// </summary>
  string Option3 { get; set; }

  /// <summary>
  /// type: string
  /// </summary>
  string Option4 { get; set; }

  /// <summary>
  /// type: string
  /// </summary>
  string Option5 { get; set; }

}
