using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "item_purchase"
/// </summary>
public interface EventItemPurchase : IGameEvent<EventItemPurchase> {

  static EventItemPurchase IGameEvent<EventItemPurchase>.Create(nint address) => new EventItemPurchaseImpl(address);

  static string IGameEvent<EventItemPurchase>.GetName() => "item_purchase";

  static uint IGameEvent<EventItemPurchase>.GetHash() => 0x4400FB1Cu;
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
  /// type: short
  /// </summary>
  short Team { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short LoadOut { get; set; }

  /// <summary>
  /// type: string
  /// </summary>
  string Weapon { get; set; }

}
