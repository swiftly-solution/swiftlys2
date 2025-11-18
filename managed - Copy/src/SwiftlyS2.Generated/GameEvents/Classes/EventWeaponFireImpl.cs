using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "weapon_fire"
/// </summary>
internal class EventWeaponFireImpl : GameEvent<EventWeaponFire>, EventWeaponFire
{

  public EventWeaponFireImpl(nint address) : base(address)
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

  // weapon name used
  public string Weapon
  { get => Accessor.GetString("weapon"); set => Accessor.SetString("weapon", value); }

  // is weapon silenced
  public bool Silenced
  { get => Accessor.GetBool("silenced"); set => Accessor.SetBool("silenced", value); }
}
