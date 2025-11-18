using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_team"
/// </summary>
public interface EventPlayerTeam : IGameEvent<EventPlayerTeam> {

  static EventPlayerTeam IGameEvent<EventPlayerTeam>.Create(nint address) => new EventPlayerTeamImpl(address);

  static string IGameEvent<EventPlayerTeam>.GetName() => "player_team";

  static uint IGameEvent<EventPlayerTeam>.GetHash() => 0xD57549C4u;
  /// <summary>
  /// player
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// player
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // player
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// player
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// team id
  /// <br/>
  /// type: byte
  /// </summary>
  byte Team { get; set; }

  /// <summary>
  /// old team id
  /// <br/>
  /// type: byte
  /// </summary>
  byte OldTeam { get; set; }

  /// <summary>
  /// team change because player disconnects
  /// <br/>
  /// type: bool
  /// </summary>
  bool Disconnect { get; set; }

  /// <summary>
  /// type: bool
  /// </summary>
  bool Silent { get; set; }

  /// <summary>
  /// type: string
  /// </summary>
  string Name { get; set; }

  /// <summary>
  /// true if player is a bot
  /// <br/>
  /// type: bool
  /// </summary>
  bool IsBot { get; set; }

}
