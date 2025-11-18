using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "broken_breakable"
/// </summary>
public interface EventBrokenBreakable : IGameEvent<EventBrokenBreakable> {

  static EventBrokenBreakable IGameEvent<EventBrokenBreakable>.Create(nint address) => new EventBrokenBreakableImpl(address);

  static string IGameEvent<EventBrokenBreakable>.GetName() => "broken_breakable";

  static uint IGameEvent<EventBrokenBreakable>.GetHash() => 0x3EBE8AE8u;
  /// <summary>
  /// type: long
  /// </summary>
  int EntIndex { get; set; }

  /// <summary>
  /// <br/>
  /// type: player_pawn
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// <br/>
  /// type: player_pawn
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// <br/>
  /// type: player_pawn
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// BREAK_GLASS, BREAK_WOOD, etc
  /// <br/>
  /// type: byte
  /// </summary>
  byte Material { get; set; }

}
