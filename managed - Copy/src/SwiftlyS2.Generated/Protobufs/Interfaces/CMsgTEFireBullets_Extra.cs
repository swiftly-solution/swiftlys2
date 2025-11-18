
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgTEFireBullets_Extra : ITypedProtobuf<CMsgTEFireBullets_Extra>
{
  static CMsgTEFireBullets_Extra ITypedProtobuf<CMsgTEFireBullets_Extra>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTEFireBullets_ExtraImpl(handle, isManuallyAllocated);


  public QAngle AimPunch { get; set; }


  public int AttackTickCount { get; set; }


  public float AttackTickFrac { get; set; }


  public int RenderTickCount { get; set; }


  public float RenderTickFrac { get; set; }


  public float InaccuracyMove { get; set; }


  public float InaccuracyAir { get; set; }


  public int Type { get; set; }

}
