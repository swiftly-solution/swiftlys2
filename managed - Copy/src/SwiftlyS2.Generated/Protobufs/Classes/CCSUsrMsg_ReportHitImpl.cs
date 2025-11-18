
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_ReportHitImpl : NetMessage<CCSUsrMsg_ReportHit>, CCSUsrMsg_ReportHit
{
  public CCSUsrMsg_ReportHitImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public float PosX
  { get => Accessor.GetFloat("pos_x"); set => Accessor.SetFloat("pos_x", value); }


  public float PosY
  { get => Accessor.GetFloat("pos_y"); set => Accessor.SetFloat("pos_y", value); }


  public float Timestamp
  { get => Accessor.GetFloat("timestamp"); set => Accessor.SetFloat("timestamp", value); }


  public float PosZ
  { get => Accessor.GetFloat("pos_z"); set => Accessor.SetFloat("pos_z", value); }

}
