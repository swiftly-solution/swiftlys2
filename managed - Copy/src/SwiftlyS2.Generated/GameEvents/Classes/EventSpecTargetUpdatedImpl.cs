using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "spec_target_updated"
/// </summary>
internal class EventSpecTargetUpdatedImpl : GameEvent<EventSpecTargetUpdated>, EventSpecTargetUpdated
{

  public EventSpecTargetUpdatedImpl(nint address) : base(address)
  {
  }

  // spectating player
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // spectating player
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // spectating player
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // spectating player
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  // ehandle of the target
  public nint Target
  { get => Accessor.GetPtr("target"); set => Accessor.SetPtr("target", value); }
}
