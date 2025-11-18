using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_full_update"
/// </summary>
public interface EventPlayerFullUpdate : IGameEvent<EventPlayerFullUpdate> {

  static EventPlayerFullUpdate IGameEvent<EventPlayerFullUpdate>.Create(nint address) => new EventPlayerFullUpdateImpl(address);

  static string IGameEvent<EventPlayerFullUpdate>.GetName() => "player_full_update";

  static uint IGameEvent<EventPlayerFullUpdate>.GetHash() => 0xC12FF460u;
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

  /// <summary>
  /// Number of this full update
  /// <br/>
  /// type: short
  /// </summary>
  short Count { get; set; }

}
