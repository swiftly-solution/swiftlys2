using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "hltv_message"
/// a HLTV message send by moderators
/// </summary>
internal class EventHltvMessageImpl : GameEvent<EventHltvMessage>, EventHltvMessage
{

  public EventHltvMessageImpl(nint address) : base(address)
  {
  }

  public string Text
  { get => Accessor.GetString("text"); set => Accessor.SetString("text", value); }
}
