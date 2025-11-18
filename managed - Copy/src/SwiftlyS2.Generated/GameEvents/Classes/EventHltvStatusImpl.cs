using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "hltv_status"
/// general HLTV status
/// </summary>
internal class EventHltvStatusImpl : GameEvent<EventHltvStatus>, EventHltvStatus
{

  public EventHltvStatusImpl(nint address) : base(address)
  {
  }

  // number of HLTV spectators
  public int Clients
  { get => Accessor.GetInt32("clients"); set => Accessor.SetInt32("clients", value); }

  // number of HLTV slots
  public int Slots
  { get => Accessor.GetInt32("slots"); set => Accessor.SetInt32("slots", value); }

  // number of HLTV proxies
  public short Proxies
  { get => (short)Accessor.GetInt32("proxies"); set => Accessor.SetInt32("proxies", value); }

  // disptach master IP:port
  public string Master
  { get => Accessor.GetString("master"); set => Accessor.SetString("master", value); }
}
