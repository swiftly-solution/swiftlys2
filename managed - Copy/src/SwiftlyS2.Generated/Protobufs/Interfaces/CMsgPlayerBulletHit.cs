
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgPlayerBulletHit : ITypedProtobuf<CMsgPlayerBulletHit>
{
  static CMsgPlayerBulletHit ITypedProtobuf<CMsgPlayerBulletHit>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgPlayerBulletHitImpl(handle, isManuallyAllocated);


  public int AttackerSlot { get; set; }


  public int VictimSlot { get; set; }


  public Vector VictimPos { get; set; }


  public int HitGroup { get; set; }


  public int Damage { get; set; }


  public int PenetrationCount { get; set; }


  public bool IsKill { get; set; }

}
