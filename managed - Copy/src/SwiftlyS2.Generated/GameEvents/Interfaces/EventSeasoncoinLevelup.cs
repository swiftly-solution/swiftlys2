using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "seasoncoin_levelup"
/// </summary>
public interface EventSeasoncoinLevelup : IGameEvent<EventSeasoncoinLevelup> {

  static EventSeasoncoinLevelup IGameEvent<EventSeasoncoinLevelup>.Create(nint address) => new EventSeasoncoinLevelupImpl(address);

  static string IGameEvent<EventSeasoncoinLevelup>.GetName() => "seasoncoin_levelup";

  static uint IGameEvent<EventSeasoncoinLevelup>.GetHash() => 0xF0EAD821u;
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
  short Category { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short Rank { get; set; }

}
