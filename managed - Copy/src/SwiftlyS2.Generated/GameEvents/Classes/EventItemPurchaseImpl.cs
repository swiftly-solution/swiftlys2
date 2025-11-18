using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "item_purchase"
/// </summary>
internal class EventItemPurchaseImpl : GameEvent<EventItemPurchase>, EventItemPurchase
{

  public EventItemPurchaseImpl(nint address) : base(address)
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

  public short Team
  { get => (short)Accessor.GetInt32("team"); set => Accessor.SetInt32("team", value); }

  public short LoadOut
  { get => (short)Accessor.GetInt32("loadout"); set => Accessor.SetInt32("loadout", value); }

  public string Weapon
  { get => Accessor.GetString("weapon"); set => Accessor.SetString("weapon", value); }
}
