using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "bullet_flight_resolution"
/// </summary>
internal class EventBulletFlightResolutionImpl : GameEvent<EventBulletFlightResolution>, EventBulletFlightResolution
{

  public EventBulletFlightResolutionImpl(nint address) : base(address)
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

  public short PosX
  { get => (short)Accessor.GetInt32("pos_x"); set => Accessor.SetInt32("pos_x", value); }

  public short PosY
  { get => (short)Accessor.GetInt32("pos_y"); set => Accessor.SetInt32("pos_y", value); }

  public short PosZ
  { get => (short)Accessor.GetInt32("pos_z"); set => Accessor.SetInt32("pos_z", value); }

  public short AngX
  { get => (short)Accessor.GetInt32("ang_x"); set => Accessor.SetInt32("ang_x", value); }

  public short AngY
  { get => (short)Accessor.GetInt32("ang_y"); set => Accessor.SetInt32("ang_y", value); }

  public short AngZ
  { get => (short)Accessor.GetInt32("ang_z"); set => Accessor.SetInt32("ang_z", value); }

  public short StartX
  { get => (short)Accessor.GetInt32("start_x"); set => Accessor.SetInt32("start_x", value); }

  public short StartY
  { get => (short)Accessor.GetInt32("start_y"); set => Accessor.SetInt32("start_y", value); }

  public short StartZ
  { get => (short)Accessor.GetInt32("start_z"); set => Accessor.SetInt32("start_z", value); }
}
