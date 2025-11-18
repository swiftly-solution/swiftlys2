
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_ShootInfoImpl : NetMessage<CCSUsrMsg_ShootInfo>, CCSUsrMsg_ShootInfo
{
  public CCSUsrMsg_ShootInfoImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int FrameNumber
  { get => Accessor.GetInt32("frame_number"); set => Accessor.SetInt32("frame_number", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgTransform> HitboxTransforms
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgTransform>(Accessor, "hitbox_transforms"); }


  public Vector ShootPos
  { get => Accessor.GetVector("shoot_pos"); set => Accessor.SetVector("shoot_pos", value); }


  public QAngle ShootDir
  { get => Accessor.GetQAngle("shoot_dir"); set => Accessor.SetQAngle("shoot_dir", value); }

}
