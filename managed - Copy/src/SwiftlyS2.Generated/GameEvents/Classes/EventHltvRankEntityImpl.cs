using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "hltv_rank_entity"
/// an entity ranking
/// </summary>
internal class EventHltvRankEntityImpl : GameEvent<EventHltvRankEntity>, EventHltvRankEntity
{

  public EventHltvRankEntityImpl(nint address) : base(address)
  {
  }

  // player slot
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // player slot
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // player slot
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // player slot
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  // ranking, how interesting is this entity to view
  public float Rank
  { get => Accessor.GetFloat("rank"); set => Accessor.SetFloat("rank", value); }

  // best/closest target entity
  public int Target
  { get => Accessor.GetPlayerSlot("target"); set => Accessor.SetPlayerSlot("target", value); }
}
