using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "show_survival_respawn_status"
/// </summary>
internal class EventShowSurvivalRespawnStatusImpl : GameEvent<EventShowSurvivalRespawnStatus>, EventShowSurvivalRespawnStatus
{

  public EventShowSurvivalRespawnStatusImpl(nint address) : base(address)
  {
  }

  public string LocToken
  { get => Accessor.GetString("loc_token"); set => Accessor.SetString("loc_token", value); }

  public int Duration
  { get => Accessor.GetInt32("duration"); set => Accessor.SetInt32("duration", value); }

  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }
}
