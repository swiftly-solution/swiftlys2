using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "defuser_pickup"
/// </summary>
internal class EventDefuserPickupImpl : GameEvent<EventDefuserPickup>, EventDefuserPickup
{

  public EventDefuserPickupImpl(nint address) : base(address)
  {
  }

  // defuser's entity ID
  public int EntityID
  { get => Accessor.GetInt32("entityid"); set => Accessor.SetInt32("entityid", value); }

  // player who picked up the defuser
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // player who picked up the defuser
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // player who picked up the defuser
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // player who picked up the defuser
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }
}
