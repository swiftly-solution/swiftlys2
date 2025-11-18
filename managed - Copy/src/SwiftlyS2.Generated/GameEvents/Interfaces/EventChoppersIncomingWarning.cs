using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "choppers_incoming_warning"
/// </summary>
public interface EventChoppersIncomingWarning : IGameEvent<EventChoppersIncomingWarning> {

  static EventChoppersIncomingWarning IGameEvent<EventChoppersIncomingWarning>.Create(nint address) => new EventChoppersIncomingWarningImpl(address);

  static string IGameEvent<EventChoppersIncomingWarning>.GetName() => "choppers_incoming_warning";

  static uint IGameEvent<EventChoppersIncomingWarning>.GetHash() => 0x68E589D1u;
  /// <summary>
  /// type: bool
  /// </summary>
  bool Global { get; set; }

}
