using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "game_message"
/// a message send by game logic to everyone
/// </summary>
internal class EventGameMessageImpl : GameEvent<EventGameMessage>, EventGameMessage
{

  public EventGameMessageImpl(nint address) : base(address)
  {
  }

  // 0 = console, 1 = HUD
  public byte Target
  { get => (byte)Accessor.GetInt32("target"); set => Accessor.SetInt32("target", value); }

  // the message text
  public string Text
  { get => Accessor.GetString("text"); set => Accessor.SetString("text", value); }
}
