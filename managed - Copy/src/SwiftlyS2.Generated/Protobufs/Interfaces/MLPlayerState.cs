
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface MLPlayerState : ITypedProtobuf<MLPlayerState>
{
  static MLPlayerState ITypedProtobuf<MLPlayerState>.Wrap(nint handle, bool isManuallyAllocated) => new MLPlayerStateImpl(handle, isManuallyAllocated);


  public int AccountId { get; set; }


  public int PlayerSlot { get; set; }


  public int Entindex { get; set; }


  public string Name { get; set; }


  public string Clan { get; set; }


  public ETeam Team { get; set; }


  public Vector Abspos { get; set; }


  public QAngle Eyeangle { get; set; }


  public Vector EyeangleFwd { get; set; }


  public int Health { get; set; }


  public int Armor { get; set; }


  public float Flashed { get; set; }


  public float Smoked { get; set; }


  public int Money { get; set; }


  public int RoundKills { get; set; }


  public int RoundKillhs { get; set; }


  public float Burning { get; set; }


  public bool Helmet { get; set; }


  public bool DefuseKit { get; set; }


  public IProtobufRepeatedFieldSubMessageType<MLWeaponState> Weapons { get; }

}
