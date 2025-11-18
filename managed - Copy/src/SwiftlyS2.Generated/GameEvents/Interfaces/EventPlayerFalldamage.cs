using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_falldamage"
/// </summary>
public interface EventPlayerFalldamage : IGameEvent<EventPlayerFalldamage> {

  static EventPlayerFalldamage IGameEvent<EventPlayerFalldamage>.Create(nint address) => new EventPlayerFalldamageImpl(address);

  static string IGameEvent<EventPlayerFalldamage>.GetName() => "player_falldamage";

  static uint IGameEvent<EventPlayerFalldamage>.GetHash() => 0x594A7109u;
  /// <summary>
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// type: float
  /// </summary>
  float Damage { get; set; }

}
