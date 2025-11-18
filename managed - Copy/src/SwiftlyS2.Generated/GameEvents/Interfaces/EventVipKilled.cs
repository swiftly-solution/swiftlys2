using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "vip_killed"
/// </summary>
public interface EventVipKilled : IGameEvent<EventVipKilled> {

  static EventVipKilled IGameEvent<EventVipKilled>.Create(nint address) => new EventVipKilledImpl(address);

  static string IGameEvent<EventVipKilled>.GetName() => "vip_killed";

  static uint IGameEvent<EventVipKilled>.GetHash() => 0x21FB59C8u;
  /// <summary>
  /// player who was the VIP
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// player who was the VIP
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // player who was the VIP
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// player who was the VIP
  /// <br/>
  /// type: player_controller
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// user ID who killed the VIP
  /// <br/>
  /// type: player_controller
  /// </summary>
  int Attacker { get; set; }

}
