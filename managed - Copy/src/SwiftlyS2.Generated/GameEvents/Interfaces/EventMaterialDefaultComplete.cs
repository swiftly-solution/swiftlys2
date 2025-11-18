using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "material_default_complete"
/// </summary>
public interface EventMaterialDefaultComplete : IGameEvent<EventMaterialDefaultComplete> {

  static EventMaterialDefaultComplete IGameEvent<EventMaterialDefaultComplete>.Create(nint address) => new EventMaterialDefaultCompleteImpl(address);

  static string IGameEvent<EventMaterialDefaultComplete>.GetName() => "material_default_complete";

  static uint IGameEvent<EventMaterialDefaultComplete>.GetHash() => 0x6235E5E8u;
}
