using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "loot_crate_opened"
/// </summary>
internal class EventLootCrateOpenedImpl : GameEvent<EventLootCrateOpened>, EventLootCrateOpened
{

  public EventLootCrateOpenedImpl(nint address) : base(address)
  {
  }

  // player entindex
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // player entindex
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // player entindex
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // player entindex
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  // type of crate (metal, wood, or paradrop)
  public string Type
  { get => Accessor.GetString("type"); set => Accessor.SetString("type", value); }
}
