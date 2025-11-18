using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hltv_replay"
/// </summary>
public interface EventHltvReplay : IGameEvent<EventHltvReplay> {

  static EventHltvReplay IGameEvent<EventHltvReplay>.Create(nint address) => new EventHltvReplayImpl(address);

  static string IGameEvent<EventHltvReplay>.GetName() => "hltv_replay";

  static uint IGameEvent<EventHltvReplay>.GetHash() => 0xC117A4EBu;
  /// <summary>
  /// number of seconds in killer replay delay
  /// <br/>
  /// type: long
  /// </summary>
  int Delay { get; set; }

  /// <summary>
  /// reason for replay	(ReplayEventType_t)
  /// <br/>
  /// type: long
  /// </summary>
  int Reason { get; set; }

}
