using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "item_pickup_failed"
/// </summary>
public interface EventItemPickupFailed : IGameEvent<EventItemPickupFailed> {

  static EventItemPickupFailed IGameEvent<EventItemPickupFailed>.Create(nint address) => new EventItemPickupFailedImpl(address);

  static string IGameEvent<EventItemPickupFailed>.GetName() => "item_pickup_failed";

  static uint IGameEvent<EventItemPickupFailed>.GetHash() => 0x0F6D19A9u;
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

  /// <summary>
  /// type: string
  /// </summary>
  string Item { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short Reason { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short Limit { get; set; }

}
