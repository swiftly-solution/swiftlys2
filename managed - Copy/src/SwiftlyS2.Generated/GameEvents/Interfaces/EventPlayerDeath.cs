using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_death"
/// a game event, name may be 32 charaters long
/// </summary>
public interface EventPlayerDeath : IGameEvent<EventPlayerDeath> {

  static EventPlayerDeath IGameEvent<EventPlayerDeath>.Create(nint address) => new EventPlayerDeathImpl(address);

  static string IGameEvent<EventPlayerDeath>.GetName() => "player_death";

  static uint IGameEvent<EventPlayerDeath>.GetHash() => 0xA6ABE875u;
  /// <summary>
  /// user ID who died
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// user ID who died
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // user ID who died
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// user ID who died
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// user ID who killed
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  int Attacker { get; set; }

  /// <summary>
  /// player who assisted in the kill
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  int Assister { get; set; }

  /// <summary>
  /// assister helped with a flash
  /// <br/>
  /// type: bool
  /// </summary>
  bool AssistedFlash { get; set; }

  /// <summary>
  /// weapon name killer used
  /// <br/>
  /// type: string
  /// </summary>
  string Weapon { get; set; }

  /// <summary>
  /// inventory item id of weapon killer used
  /// <br/>
  /// type: string
  /// </summary>
  string WeaponItemid { get; set; }

  /// <summary>
  /// faux item id of weapon killer used
  /// <br/>
  /// type: string
  /// </summary>
  string WeaponFauxitemid { get; set; }

  /// <summary>
  /// type: string
  /// </summary>
  string WeaponOriginalownerXuid { get; set; }

  /// <summary>
  /// singals a headshot
  /// <br/>
  /// type: bool
  /// </summary>
  bool Headshot { get; set; }

  /// <summary>
  /// did killer dominate victim with this kill
  /// <br/>
  /// type: short
  /// </summary>
  short Dominated { get; set; }

  /// <summary>
  /// did killer get revenge on victim with this kill
  /// <br/>
  /// type: short
  /// </summary>
  short Revenge { get; set; }

  /// <summary>
  /// is the kill resulting in squad wipe
  /// <br/>
  /// type: short
  /// </summary>
  short Wipe { get; set; }

  /// <summary>
  /// number of objects shot penetrated before killing target
  /// <br/>
  /// type: short
  /// </summary>
  short Penetrated { get; set; }

  /// <summary>
  /// if replay data is unavailable, this will be present and set to false
  /// <br/>
  /// type: bool
  /// </summary>
  bool NoReplay { get; set; }

  /// <summary>
  /// kill happened without a scope, used for death notice icon
  /// <br/>
  /// type: bool
  /// </summary>
  bool NoScope { get; set; }

  /// <summary>
  /// hitscan weapon went through smoke grenade
  /// <br/>
  /// type: bool
  /// </summary>
  bool ThruSmoke { get; set; }

  /// <summary>
  /// attacker was blind from flashbang
  /// <br/>
  /// type: bool
  /// </summary>
  bool AttackerBlind { get; set; }

  /// <summary>
  /// distance to victim in meters
  /// <br/>
  /// type: float
  /// </summary>
  float Distance { get; set; }

  /// <summary>
  /// damage done to health
  /// <br/>
  /// type: short
  /// </summary>
  short DmgHealth { get; set; }

  /// <summary>
  /// damage done to armor
  /// <br/>
  /// type: byte
  /// </summary>
  byte DmgArmor { get; set; }

  /// <summary>
  /// hitgroup that was damaged
  /// <br/>
  /// type: byte
  /// </summary>
  byte HitGroup { get; set; }

  /// <summary>
  /// attacker was in midair
  /// <br/>
  /// type: bool
  /// </summary>
  bool AttackerInAir { get; set; }

}
