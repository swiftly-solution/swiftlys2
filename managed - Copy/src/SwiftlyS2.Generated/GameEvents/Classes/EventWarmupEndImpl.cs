using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "warmup_end"
/// </summary>
internal class EventWarmupEndImpl : GameEvent<EventWarmupEnd>, EventWarmupEnd
{

  public EventWarmupEndImpl(nint address) : base(address)
  {
  }
}
