using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_connect_full"
/// player has sent final message in the connection sequence
/// </summary>
public interface EventPlayerConnectFull : IGameEvent<EventPlayerConnectFull> {

  static EventPlayerConnectFull IGameEvent<EventPlayerConnectFull>.Create(nint address) => new EventPlayerConnectFullImpl(address);

  static string IGameEvent<EventPlayerConnectFull>.GetName() => "player_connect_full";

  static uint IGameEvent<EventPlayerConnectFull>.GetHash() => 0x5BE3B233u;
  /// <summary>
  /// user ID on server (unique on server)
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// user ID on server (unique on server)
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // user ID on server (unique on server)
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// user ID on server (unique on server)
  /// <br/>
  /// type: player_controller
  /// </summary>
  int UserId { get; set; }

}
