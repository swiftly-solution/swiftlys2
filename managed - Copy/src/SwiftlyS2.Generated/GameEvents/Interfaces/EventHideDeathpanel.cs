using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hide_deathpanel"
/// </summary>
public interface EventHideDeathpanel : IGameEvent<EventHideDeathpanel> {

  static EventHideDeathpanel IGameEvent<EventHideDeathpanel>.Create(nint address) => new EventHideDeathpanelImpl(address);

  static string IGameEvent<EventHideDeathpanel>.GetName() => "hide_deathpanel";

  static uint IGameEvent<EventHideDeathpanel>.GetHash() => 0x3B386392u;
}
