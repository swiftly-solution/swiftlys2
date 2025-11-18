using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "read_game_titledata"
/// read user titledata from profile
/// </summary>
internal class EventReadGameTitledataImpl : GameEvent<EventReadGameTitledata>, EventReadGameTitledata
{

  public EventReadGameTitledataImpl(nint address) : base(address)
  {
  }

  // Controller id of user
  public short ControllerId
  { get => (short)Accessor.GetInt32("controllerId"); set => Accessor.SetInt32("controllerId", value); }
}
