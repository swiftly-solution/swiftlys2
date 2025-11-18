using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "break_prop"
/// </summary>
public interface EventBreakProp : IGameEvent<EventBreakProp> {

  static EventBreakProp IGameEvent<EventBreakProp>.Create(nint address) => new EventBreakPropImpl(address);

  static string IGameEvent<EventBreakProp>.GetName() => "break_prop";

  static uint IGameEvent<EventBreakProp>.GetHash() => 0x20D10398u;
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
  /// type: bool
  /// </summary>
  bool PlayerHeld { get; set; }

  /// <summary>
  /// type: bool
  /// </summary>
  bool PlayerThrown { get; set; }

  /// <summary>
  /// type: bool
  /// </summary>
  bool PlayerDropped { get; set; }

}
