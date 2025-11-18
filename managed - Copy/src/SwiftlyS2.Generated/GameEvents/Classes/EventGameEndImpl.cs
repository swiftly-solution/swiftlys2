using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "game_end"
/// a game ended
/// </summary>
internal class EventGameEndImpl : GameEvent<EventGameEnd>, EventGameEnd
{

  public EventGameEndImpl(nint address) : base(address)
  {
  }

  // winner team/user id
  public byte Winner
  { get => (byte)Accessor.GetInt32("winner"); set => Accessor.SetInt32("winner", value); }
}
