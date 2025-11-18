using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "sfuievent"
/// </summary>
public interface EventSfuievent : IGameEvent<EventSfuievent> {

  static EventSfuievent IGameEvent<EventSfuievent>.Create(nint address) => new EventSfuieventImpl(address);

  static string IGameEvent<EventSfuievent>.GetName() => "sfuievent";

  static uint IGameEvent<EventSfuievent>.GetHash() => 0xA20ACD22u;
  /// <summary>
  /// type: string
  /// </summary>
  string Action { get; set; }

  /// <summary>
  /// type: string
  /// </summary>
  string Data { get; set; }

  /// <summary>
  /// type: byte
  /// </summary>
  byte Slot { get; set; }

}
