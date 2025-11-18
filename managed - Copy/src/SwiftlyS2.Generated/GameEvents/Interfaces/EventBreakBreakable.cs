using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "break_breakable"
/// </summary>
public interface EventBreakBreakable : IGameEvent<EventBreakBreakable> {

  static EventBreakBreakable IGameEvent<EventBreakBreakable>.Create(nint address) => new EventBreakBreakableImpl(address);

  static string IGameEvent<EventBreakBreakable>.GetName() => "break_breakable";

  static uint IGameEvent<EventBreakBreakable>.GetHash() => 0x7CBB3150u;
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
