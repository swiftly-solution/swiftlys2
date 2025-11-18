using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "drop_rate_modified"
/// </summary>
internal class EventDropRateModifiedImpl : GameEvent<EventDropRateModified>, EventDropRateModified
{

  public EventDropRateModifiedImpl(nint address) : base(address)
  {
  }
}
