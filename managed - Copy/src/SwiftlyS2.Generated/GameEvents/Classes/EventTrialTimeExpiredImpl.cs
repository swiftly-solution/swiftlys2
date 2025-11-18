using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "trial_time_expired"
/// </summary>
internal class EventTrialTimeExpiredImpl : GameEvent<EventTrialTimeExpired>, EventTrialTimeExpired
{

  public EventTrialTimeExpiredImpl(nint address) : base(address)
  {
  }

  // player whose time has expired
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // player whose time has expired
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // player whose time has expired
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // player whose time has expired
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }
}
