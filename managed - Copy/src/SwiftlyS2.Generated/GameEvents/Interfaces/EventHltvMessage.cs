using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hltv_message"
/// a HLTV message send by moderators
/// </summary>
public interface EventHltvMessage : IGameEvent<EventHltvMessage> {

  static EventHltvMessage IGameEvent<EventHltvMessage>.Create(nint address) => new EventHltvMessageImpl(address);

  static string IGameEvent<EventHltvMessage>.GetName() => "hltv_message";

  static uint IGameEvent<EventHltvMessage>.GetHash() => 0x0E93862Bu;
  /// <summary>
  /// type: string
  /// </summary>
  string Text { get; set; }

}
