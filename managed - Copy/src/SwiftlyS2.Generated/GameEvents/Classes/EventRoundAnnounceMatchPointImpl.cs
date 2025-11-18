using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "round_announce_match_point"
/// </summary>
internal class EventRoundAnnounceMatchPointImpl : GameEvent<EventRoundAnnounceMatchPoint>, EventRoundAnnounceMatchPoint
{

  public EventRoundAnnounceMatchPointImpl(nint address) : base(address)
  {
  }
}
