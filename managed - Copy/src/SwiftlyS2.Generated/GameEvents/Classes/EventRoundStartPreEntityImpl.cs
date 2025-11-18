using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "round_start_pre_entity"
/// </summary>
internal class EventRoundStartPreEntityImpl : GameEvent<EventRoundStartPreEntity>, EventRoundStartPreEntity
{

  public EventRoundStartPreEntityImpl(nint address) : base(address)
  {
  }
}
