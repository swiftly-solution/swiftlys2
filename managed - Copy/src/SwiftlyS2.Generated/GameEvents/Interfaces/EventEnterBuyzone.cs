using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "enter_buyzone"
/// </summary>
public interface EventEnterBuyzone : IGameEvent<EventEnterBuyzone> {

  static EventEnterBuyzone IGameEvent<EventEnterBuyzone>.Create(nint address) => new EventEnterBuyzoneImpl(address);

  static string IGameEvent<EventEnterBuyzone>.GetName() => "enter_buyzone";

  static uint IGameEvent<EventEnterBuyzone>.GetHash() => 0x9E49E798u;
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
  /// type: bool
  /// </summary>
  bool CanBuy { get; set; }

}
