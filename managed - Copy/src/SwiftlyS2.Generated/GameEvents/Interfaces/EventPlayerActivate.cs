using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_activate"
/// </summary>
public interface EventPlayerActivate : IGameEvent<EventPlayerActivate> {

  static EventPlayerActivate IGameEvent<EventPlayerActivate>.Create(nint address) => new EventPlayerActivateImpl(address);

  static string IGameEvent<EventPlayerActivate>.GetName() => "player_activate";

  static uint IGameEvent<EventPlayerActivate>.GetHash() => 0x5C0DF448u;
  /// <summary>
  /// user ID on server
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// user ID on server
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // user ID on server
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// user ID on server
  /// <br/>
  /// type: player_controller
  /// </summary>
  int UserId { get; set; }

}
