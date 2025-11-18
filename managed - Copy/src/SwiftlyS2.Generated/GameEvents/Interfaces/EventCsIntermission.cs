using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "cs_intermission"
/// </summary>
public interface EventCsIntermission : IGameEvent<EventCsIntermission> {

  static EventCsIntermission IGameEvent<EventCsIntermission>.Create(nint address) => new EventCsIntermissionImpl(address);

  static string IGameEvent<EventCsIntermission>.GetName() => "cs_intermission";

  static uint IGameEvent<EventCsIntermission>.GetHash() => 0x1BDF3E80u;
}
