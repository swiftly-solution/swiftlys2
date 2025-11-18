using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "game_start"
/// a new game starts
/// </summary>
public interface EventGameStart : IGameEvent<EventGameStart> {

  static EventGameStart IGameEvent<EventGameStart>.Create(nint address) => new EventGameStartImpl(address);

  static string IGameEvent<EventGameStart>.GetName() => "game_start";

  static uint IGameEvent<EventGameStart>.GetHash() => 0x8A6A0374u;
  /// <summary>
  /// max round
  /// <br/>
  /// type: long
  /// </summary>
  int RoundsLimit { get; set; }

  /// <summary>
  /// time limit
  /// <br/>
  /// type: long
  /// </summary>
  int TimeLimit { get; set; }

  /// <summary>
  /// frag limit
  /// <br/>
  /// type: long
  /// </summary>
  int FragLimit { get; set; }

  /// <summary>
  /// round objective
  /// <br/>
  /// type: string
  /// </summary>
  string Objective { get; set; }

}
