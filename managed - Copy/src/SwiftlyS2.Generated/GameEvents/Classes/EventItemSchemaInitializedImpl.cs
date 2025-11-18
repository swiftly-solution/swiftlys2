using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "item_schema_initialized"
/// </summary>
internal class EventItemSchemaInitializedImpl : GameEvent<EventItemSchemaInitialized>, EventItemSchemaInitialized
{

  public EventItemSchemaInitializedImpl(nint address) : base(address)
  {
  }
}
