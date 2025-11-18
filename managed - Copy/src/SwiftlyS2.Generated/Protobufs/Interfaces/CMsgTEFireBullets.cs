
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgTEFireBullets : ITypedProtobuf<CMsgTEFireBullets>, INetMessage<CMsgTEFireBullets>, IDisposable
{
  static int INetMessage<CMsgTEFireBullets>.MessageId => 452;
  
  static string INetMessage<CMsgTEFireBullets>.MessageName => "CMsgTEFireBullets";

  static CMsgTEFireBullets ITypedProtobuf<CMsgTEFireBullets>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTEFireBulletsImpl(handle, isManuallyAllocated);


  public Vector Origin { get; set; }


  public QAngle Angles { get; set; }


  public uint WeaponId { get; set; }


  public uint Mode { get; set; }


  public uint Seed { get; set; }


  public uint Player { get; set; }


  public float Inaccuracy { get; set; }


  public float RecoilIndex { get; set; }


  public float Spread { get; set; }


  public int SoundType { get; set; }


  public uint ItemDefIndex { get; set; }


  public uint SoundDspEffect { get; set; }


  public Vector EntOrigin { get; set; }


  public uint NumBulletsRemaining { get; set; }


  public uint AttackType { get; set; }


  public bool PlayerInair { get; set; }


  public bool PlayerScoped { get; set; }


  public int Tick { get; set; }


  public CMsgTEFireBullets_Extra Extra { get; }

}
