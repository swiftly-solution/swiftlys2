using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_info"
/// a player changed his name
/// </summary>
public interface EventPlayerInfo : IGameEvent<EventPlayerInfo> {

  static EventPlayerInfo IGameEvent<EventPlayerInfo>.Create(nint address) => new EventPlayerInfoImpl(address);

  static string IGameEvent<EventPlayerInfo>.GetName() => "player_info";

  static uint IGameEvent<EventPlayerInfo>.GetHash() => 0x0A0BAFFDu;
  /// <summary>
  /// player name
  /// <br/>
  /// type: string
  /// </summary>
  string Name { get; set; }

  /// <summary>
  /// user ID on server (unique on server)
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// user ID on server (unique on server)
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // user ID on server (unique on server)
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// user ID on server (unique on server)
  /// <br/>
  /// type: player_controller
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// player network (i.e steam) id
  /// <br/>
  /// type: uint64
  /// </summary>
  ulong SteamID { get; set; }

  /// <summary>
  /// true if player is a AI bot
  /// <br/>
  /// type: bool
  /// </summary>
  bool Bot { get; set; }

}
