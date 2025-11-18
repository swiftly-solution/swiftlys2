using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "reset_game_titledata"
/// reset user titledata; do not automatically write profile
/// </summary>
internal class EventResetGameTitledataImpl : GameEvent<EventResetGameTitledata>, EventResetGameTitledata
{

  public EventResetGameTitledataImpl(nint address) : base(address)
  {
  }

  // Controller id of user
  public short ControllerId
  { get => (short)Accessor.GetInt32("controllerId"); set => Accessor.SetInt32("controllerId", value); }
}
