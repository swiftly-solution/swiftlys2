using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "door_close"
/// </summary>
public interface EventDoorClose : IGameEvent<EventDoorClose> {

  static EventDoorClose IGameEvent<EventDoorClose>.Create(nint address) => new EventDoorCloseImpl(address);

  static string IGameEvent<EventDoorClose>.GetName() => "door_close";

  static uint IGameEvent<EventDoorClose>.GetHash() => 0xC96E7A7Eu;
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
  /// Is the door a checkpoint door
  /// <br/>
  /// type: bool
  /// </summary>
  bool Checkpoint { get; set; }

}
