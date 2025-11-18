using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "seasoncoin_levelup"
/// </summary>
internal class EventSeasoncoinLevelupImpl : GameEvent<EventSeasoncoinLevelup>, EventSeasoncoinLevelup
{

  public EventSeasoncoinLevelupImpl(nint address) : base(address)
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

  public short Category
  { get => (short)Accessor.GetInt32("category"); set => Accessor.SetInt32("category", value); }

  public short Rank
  { get => (short)Accessor.GetInt32("rank"); set => Accessor.SetInt32("rank", value); }
}
