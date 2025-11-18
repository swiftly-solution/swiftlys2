using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "teamplay_broadcast_audio"
/// emits a sound to everyone on a team
/// </summary>
public interface EventTeamplayBroadcastAudio : IGameEvent<EventTeamplayBroadcastAudio> {

  static EventTeamplayBroadcastAudio IGameEvent<EventTeamplayBroadcastAudio>.Create(nint address) => new EventTeamplayBroadcastAudioImpl(address);

  static string IGameEvent<EventTeamplayBroadcastAudio>.GetName() => "teamplay_broadcast_audio";

  static uint IGameEvent<EventTeamplayBroadcastAudio>.GetHash() => 0xB81F833Bu;
  /// <summary>
  /// unique team id
  /// <br/>
  /// type: byte
  /// </summary>
  byte Team { get; set; }

  /// <summary>
  /// name of the sound to emit
  /// <br/>
  /// type: string
  /// </summary>
  string Sound { get; set; }

}
