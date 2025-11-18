using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "hostage_stops_following"
/// </summary>
internal class EventHostageStopsFollowingImpl : GameEvent<EventHostageStopsFollowing>, EventHostageStopsFollowing
{

  public EventHostageStopsFollowingImpl(nint address) : base(address)
  {
  }

  // player who rescued the hostage
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // player who rescued the hostage
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // player who rescued the hostage
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // player who rescued the hostage
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  // hostage entity index
  public short Hostage
  { get => (short)Accessor.GetInt32("hostage"); set => Accessor.SetInt32("hostage", value); }
}
