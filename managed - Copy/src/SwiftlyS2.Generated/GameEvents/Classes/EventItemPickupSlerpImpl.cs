using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "item_pickup_slerp"
/// </summary>
internal class EventItemPickupSlerpImpl : GameEvent<EventItemPickupSlerp>, EventItemPickupSlerp
{

  public EventItemPickupSlerpImpl(nint address) : base(address)
  {
  }

  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  public short Index
  { get => (short)Accessor.GetInt32("index"); set => Accessor.SetInt32("index", value); }

  public short Behavior
  { get => (short)Accessor.GetInt32("behavior"); set => Accessor.SetInt32("behavior", value); }
}
