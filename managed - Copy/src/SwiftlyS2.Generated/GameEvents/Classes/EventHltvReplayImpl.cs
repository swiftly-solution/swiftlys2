using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "hltv_replay"
/// </summary>
internal class EventHltvReplayImpl : GameEvent<EventHltvReplay>, EventHltvReplay
{

  public EventHltvReplayImpl(nint address) : base(address)
  {
  }

  // number of seconds in killer replay delay
  public int Delay
  { get => Accessor.GetInt32("delay"); set => Accessor.SetInt32("delay", value); }

  // reason for replay	(ReplayEventType_t)
  public int Reason
  { get => Accessor.GetInt32("reason"); set => Accessor.SetInt32("reason", value); }
}
