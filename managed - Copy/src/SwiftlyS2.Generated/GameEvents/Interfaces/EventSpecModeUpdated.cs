using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "spec_mode_updated"
/// </summary>
public interface EventSpecModeUpdated : IGameEvent<EventSpecModeUpdated> {

  static EventSpecModeUpdated IGameEvent<EventSpecModeUpdated>.Create(nint address) => new EventSpecModeUpdatedImpl(address);

  static string IGameEvent<EventSpecModeUpdated>.GetName() => "spec_mode_updated";

  static uint IGameEvent<EventSpecModeUpdated>.GetHash() => 0x25E84B54u;
  /// <summary>
  /// spectating player
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// spectating player
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // spectating player
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// spectating player
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  int UserId { get; set; }

}
