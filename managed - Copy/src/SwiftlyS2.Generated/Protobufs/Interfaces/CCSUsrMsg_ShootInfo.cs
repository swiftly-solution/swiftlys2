
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_ShootInfo : ITypedProtobuf<CCSUsrMsg_ShootInfo>, INetMessage<CCSUsrMsg_ShootInfo>, IDisposable
{
  static int INetMessage<CCSUsrMsg_ShootInfo>.MessageId => 383;
  
  static string INetMessage<CCSUsrMsg_ShootInfo>.MessageName => "CCSUsrMsg_ShootInfo";

  static CCSUsrMsg_ShootInfo ITypedProtobuf<CCSUsrMsg_ShootInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_ShootInfoImpl(handle, isManuallyAllocated);


  public int FrameNumber { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CMsgTransform> HitboxTransforms { get; }


  public Vector ShootPos { get; set; }


  public QAngle ShootDir { get; set; }

}
