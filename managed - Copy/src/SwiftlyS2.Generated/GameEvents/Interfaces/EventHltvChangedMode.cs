using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hltv_changed_mode"
/// </summary>
public interface EventHltvChangedMode : IGameEvent<EventHltvChangedMode> {

  static EventHltvChangedMode IGameEvent<EventHltvChangedMode>.Create(nint address) => new EventHltvChangedModeImpl(address);

  static string IGameEvent<EventHltvChangedMode>.GetName() => "hltv_changed_mode";

  static uint IGameEvent<EventHltvChangedMode>.GetHash() => 0x11795622u;
  /// <summary>
  /// type: long
  /// </summary>
  int OldMode { get; set; }

  /// <summary>
  /// type: long
  /// </summary>
  int NewMode { get; set; }

  /// <summary>
  /// type: long
  /// </summary>
  int ObsTarget { get; set; }

}
