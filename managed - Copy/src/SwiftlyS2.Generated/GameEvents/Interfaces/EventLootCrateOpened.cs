using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "loot_crate_opened"
/// </summary>
public interface EventLootCrateOpened : IGameEvent<EventLootCrateOpened> {

  static EventLootCrateOpened IGameEvent<EventLootCrateOpened>.Create(nint address) => new EventLootCrateOpenedImpl(address);

  static string IGameEvent<EventLootCrateOpened>.GetName() => "loot_crate_opened";

  static uint IGameEvent<EventLootCrateOpened>.GetHash() => 0x18E203D5u;
  /// <summary>
  /// player entindex
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// player entindex
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // player entindex
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// player entindex
  /// <br/>
  /// type: player_controller
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// type of crate (metal, wood, or paradrop)
  /// <br/>
  /// type: string
  /// </summary>
  string Type { get; set; }

}
