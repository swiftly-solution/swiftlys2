using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "parachute_pickup"
/// </summary>
public interface EventParachutePickup : IGameEvent<EventParachutePickup> {

  static EventParachutePickup IGameEvent<EventParachutePickup>.Create(nint address) => new EventParachutePickupImpl(address);

  static string IGameEvent<EventParachutePickup>.GetName() => "parachute_pickup";

  static uint IGameEvent<EventParachutePickup>.GetHash() => 0x9A331261u;
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
