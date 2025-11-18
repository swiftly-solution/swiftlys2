using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "ugc_map_unsubscribed"
/// </summary>
public interface EventUgcMapUnsubscribed : IGameEvent<EventUgcMapUnsubscribed> {

  static EventUgcMapUnsubscribed IGameEvent<EventUgcMapUnsubscribed>.Create(nint address) => new EventUgcMapUnsubscribedImpl(address);

  static string IGameEvent<EventUgcMapUnsubscribed>.GetName() => "ugc_map_unsubscribed";

  static uint IGameEvent<EventUgcMapUnsubscribed>.GetHash() => 0x275C0753u;
  /// <summary>
  /// type: uint64
  /// </summary>
  ulong PublishedFileId { get; set; }

}
