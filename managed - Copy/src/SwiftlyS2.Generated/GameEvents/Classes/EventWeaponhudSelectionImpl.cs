using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "weaponhud_selection"
/// </summary>
internal class EventWeaponhudSelectionImpl : GameEvent<EventWeaponhudSelection>, EventWeaponhudSelection
{

  public EventWeaponhudSelectionImpl(nint address) : base(address)
  {
  }

  // Player who this event applies to
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // Player who this event applies to
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // Player who this event applies to
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // Player who this event applies to
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  // EWeaponHudSelectionMode (switch / pickup / drop)
  public byte Mode
  { get => (byte)Accessor.GetInt32("mode"); set => Accessor.SetInt32("mode", value); }

  // Weapon entity index
  public int EntIndex
  { get => Accessor.GetInt32("entindex"); set => Accessor.SetInt32("entindex", value); }
}
