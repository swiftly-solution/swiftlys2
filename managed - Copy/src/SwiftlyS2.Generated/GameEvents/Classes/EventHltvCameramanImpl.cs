using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "hltv_cameraman"
/// a spectator/player is a cameraman
/// </summary>
internal class EventHltvCameramanImpl : GameEvent<EventHltvCameraman>, EventHltvCameraman
{

  public EventHltvCameramanImpl(nint address) : base(address)
  {
  }

  // camera man entity index
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // camera man entity index
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // camera man entity index
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // camera man entity index
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }
}
