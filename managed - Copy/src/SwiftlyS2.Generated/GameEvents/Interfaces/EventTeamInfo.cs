using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "team_info"
/// info about team
/// </summary>
public interface EventTeamInfo : IGameEvent<EventTeamInfo> {

  static EventTeamInfo IGameEvent<EventTeamInfo>.Create(nint address) => new EventTeamInfoImpl(address);

  static string IGameEvent<EventTeamInfo>.GetName() => "team_info";

  static uint IGameEvent<EventTeamInfo>.GetHash() => 0x61D50BD1u;
  /// <summary>
  /// unique team id
  /// <br/>
  /// type: byte
  /// </summary>
  byte TeamID { get; set; }

  /// <summary>
  /// team name eg "Team Blue"
  /// <br/>
  /// type: string
  /// </summary>
  string Teamname { get; set; }

}
