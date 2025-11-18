using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "item_equip"
/// </summary>
internal class EventItemEquipImpl : GameEvent<EventItemEquip>, EventItemEquip
{

  public EventItemEquipImpl(nint address) : base(address)
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

  // either a weapon such as 'tmp' or 'hegrenade', or an item such as 'nvgs'
  public string Item
  { get => Accessor.GetString("item"); set => Accessor.SetString("item", value); }

  public int DefIndex
  { get => Accessor.GetInt32("defindex"); set => Accessor.SetInt32("defindex", value); }

  public bool CanZoom
  { get => Accessor.GetBool("canzoom"); set => Accessor.SetBool("canzoom", value); }

  public bool HasSilencer
  { get => Accessor.GetBool("hassilencer"); set => Accessor.SetBool("hassilencer", value); }

  public bool IsSilenced
  { get => Accessor.GetBool("issilenced"); set => Accessor.SetBool("issilenced", value); }

  public bool HasTracers
  { get => Accessor.GetBool("hastracers"); set => Accessor.SetBool("hastracers", value); }

  public short WepType
  { get => (short)Accessor.GetInt32("weptype"); set => Accessor.SetInt32("weptype", value); }

  public bool IsPainted
  { get => Accessor.GetBool("ispainted"); set => Accessor.SetBool("ispainted", value); }
}
