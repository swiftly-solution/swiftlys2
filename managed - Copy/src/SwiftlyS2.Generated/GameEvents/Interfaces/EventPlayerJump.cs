using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_jump"
/// </summary>
public interface EventPlayerJump : IGameEvent<EventPlayerJump> {

  static EventPlayerJump IGameEvent<EventPlayerJump>.Create(nint address) => new EventPlayerJumpImpl(address);

  static string IGameEvent<EventPlayerJump>.GetName() => "player_jump";

  static uint IGameEvent<EventPlayerJump>.GetHash() => 0xA8C90F75u;
  /// <summary>
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// <br/>
  /// type: player_controller
  /// </summary>
  int UserId { get; set; }

}
