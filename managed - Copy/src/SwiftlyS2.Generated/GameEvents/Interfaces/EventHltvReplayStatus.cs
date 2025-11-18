using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hltv_replay_status"
/// </summary>
public interface EventHltvReplayStatus : IGameEvent<EventHltvReplayStatus> {

  static EventHltvReplayStatus IGameEvent<EventHltvReplayStatus>.Create(nint address) => new EventHltvReplayStatusImpl(address);

  static string IGameEvent<EventHltvReplayStatus>.GetName() => "hltv_replay_status";

  static uint IGameEvent<EventHltvReplayStatus>.GetHash() => 0x262D2D46u;
  /// <summary>
  /// reason for hltv replay status change ()
  /// <br/>
  /// type: long
  /// </summary>
  int Reason { get; set; }

}
