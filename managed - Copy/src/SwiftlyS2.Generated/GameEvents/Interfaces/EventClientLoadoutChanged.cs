using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "client_loadout_changed"
/// </summary>
public interface EventClientLoadoutChanged : IGameEvent<EventClientLoadoutChanged> {

  static EventClientLoadoutChanged IGameEvent<EventClientLoadoutChanged>.Create(nint address) => new EventClientLoadoutChangedImpl(address);

  static string IGameEvent<EventClientLoadoutChanged>.GetName() => "client_loadout_changed";

  static uint IGameEvent<EventClientLoadoutChanged>.GetHash() => 0x2C16C0BAu;
}
