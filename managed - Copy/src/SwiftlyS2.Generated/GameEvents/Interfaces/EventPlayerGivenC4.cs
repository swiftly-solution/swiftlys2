using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_given_c4"
/// </summary>
public interface EventPlayerGivenC4 : IGameEvent<EventPlayerGivenC4> {

  static EventPlayerGivenC4 IGameEvent<EventPlayerGivenC4>.Create(nint address) => new EventPlayerGivenC4Impl(address);

  static string IGameEvent<EventPlayerGivenC4>.GetName() => "player_given_c4";

  static uint IGameEvent<EventPlayerGivenC4>.GetHash() => 0x0491CF9Cu;
  /// <summary>
  /// user ID who received the c4
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// user ID who received the c4
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // user ID who received the c4
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// user ID who received the c4
  /// <br/>
  /// type: player_controller
  /// </summary>
  int UserId { get; set; }

}
