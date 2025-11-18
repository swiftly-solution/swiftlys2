using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "inferno_expire"
/// </summary>
public interface EventInfernoExpire : IGameEvent<EventInfernoExpire> {

  static EventInfernoExpire IGameEvent<EventInfernoExpire>.Create(nint address) => new EventInfernoExpireImpl(address);

  static string IGameEvent<EventInfernoExpire>.GetName() => "inferno_expire";

  static uint IGameEvent<EventInfernoExpire>.GetHash() => 0x6C556CEEu;
  /// <summary>
  /// type: short
  /// </summary>
  short EntityID { get; set; }

  /// <summary>
  /// type: float
  /// </summary>
  float X { get; set; }

  /// <summary>
  /// type: float
  /// </summary>
  float Y { get; set; }

  /// <summary>
  /// type: float
  /// </summary>
  float Z { get; set; }

}
