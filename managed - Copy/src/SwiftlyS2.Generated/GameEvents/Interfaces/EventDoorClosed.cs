using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "door_closed"
/// </summary>
public interface EventDoorClosed : IGameEvent<EventDoorClosed> {

  static EventDoorClosed IGameEvent<EventDoorClosed>.Create(nint address) => new EventDoorClosedImpl(address);

  static string IGameEvent<EventDoorClosed>.GetName() => "door_closed";

  static uint IGameEvent<EventDoorClosed>.GetHash() => 0x32EA36EEu;
  /// <summary>
  /// Who closed the door
  /// <br/>
  /// type: player_pawn
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// Who closed the door
  /// <br/>
  /// type: player_pawn
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // Who closed the door
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// Who closed the door
  /// <br/>
  /// type: player_pawn
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// type: long
  /// </summary>
  int EntIndex { get; set; }

}
