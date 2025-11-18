using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "add_player_sonar_icon"
/// </summary>
internal class EventAddPlayerSonarIconImpl : GameEvent<EventAddPlayerSonarIcon>, EventAddPlayerSonarIcon
{

  public EventAddPlayerSonarIconImpl(nint address) : base(address)
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

  public float PosX
  { get => Accessor.GetFloat("pos_x"); set => Accessor.SetFloat("pos_x", value); }

  public float PosY
  { get => Accessor.GetFloat("pos_y"); set => Accessor.SetFloat("pos_y", value); }

  public float PosZ
  { get => Accessor.GetFloat("pos_z"); set => Accessor.SetFloat("pos_z", value); }
}
