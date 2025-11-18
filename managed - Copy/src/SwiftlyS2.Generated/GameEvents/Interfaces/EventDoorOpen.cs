using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "door_open"
/// </summary>
public interface EventDoorOpen : IGameEvent<EventDoorOpen> {

  static EventDoorOpen IGameEvent<EventDoorOpen>.Create(nint address) => new EventDoorOpenImpl(address);

  static string IGameEvent<EventDoorOpen>.GetName() => "door_open";

  static uint IGameEvent<EventDoorOpen>.GetHash() => 0x062A102Au;
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
