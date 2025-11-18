
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class MLPlayerStateImpl : TypedProtobuf<MLPlayerState>, MLPlayerState
{
  public MLPlayerStateImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int AccountId
  { get => Accessor.GetInt32("account_id"); set => Accessor.SetInt32("account_id", value); }


  public int PlayerSlot
  { get => Accessor.GetInt32("player_slot"); set => Accessor.SetInt32("player_slot", value); }


  public int Entindex
  { get => Accessor.GetInt32("entindex"); set => Accessor.SetInt32("entindex", value); }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public string Clan
  { get => Accessor.GetString("clan"); set => Accessor.SetString("clan", value); }


  public ETeam Team
  { get => (ETeam)Accessor.GetInt32("team"); set => Accessor.SetInt32("team", (int)value); }


  public Vector Abspos
  { get => Accessor.GetVector("abspos"); set => Accessor.SetVector("abspos", value); }


  public QAngle Eyeangle
  { get => Accessor.GetQAngle("eyeangle"); set => Accessor.SetQAngle("eyeangle", value); }


  public Vector EyeangleFwd
  { get => Accessor.GetVector("eyeangle_fwd"); set => Accessor.SetVector("eyeangle_fwd", value); }


  public int Health
  { get => Accessor.GetInt32("health"); set => Accessor.SetInt32("health", value); }


  public int Armor
  { get => Accessor.GetInt32("armor"); set => Accessor.SetInt32("armor", value); }


  public float Flashed
  { get => Accessor.GetFloat("flashed"); set => Accessor.SetFloat("flashed", value); }


  public float Smoked
  { get => Accessor.GetFloat("smoked"); set => Accessor.SetFloat("smoked", value); }


  public int Money
  { get => Accessor.GetInt32("money"); set => Accessor.SetInt32("money", value); }


  public int RoundKills
  { get => Accessor.GetInt32("round_kills"); set => Accessor.SetInt32("round_kills", value); }


  public int RoundKillhs
  { get => Accessor.GetInt32("round_killhs"); set => Accessor.SetInt32("round_killhs", value); }


  public float Burning
  { get => Accessor.GetFloat("burning"); set => Accessor.SetFloat("burning", value); }


  public bool Helmet
  { get => Accessor.GetBool("helmet"); set => Accessor.SetBool("helmet", value); }


  public bool DefuseKit
  { get => Accessor.GetBool("defuse_kit"); set => Accessor.SetBool("defuse_kit", value); }


  public IProtobufRepeatedFieldSubMessageType<MLWeaponState> Weapons
  { get => new ProtobufRepeatedFieldSubMessageType<MLWeaponState>(Accessor, "weapons"); }

}
