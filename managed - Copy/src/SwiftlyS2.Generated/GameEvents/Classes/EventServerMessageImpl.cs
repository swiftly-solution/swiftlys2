using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "server_message"
/// a generic server message
/// </summary>
internal class EventServerMessageImpl : GameEvent<EventServerMessage>, EventServerMessage
{

  public EventServerMessageImpl(nint address) : base(address)
  {
  }

  // the message text
  public string Text
  { get => Accessor.GetString("text"); set => Accessor.SetString("text", value); }
}
