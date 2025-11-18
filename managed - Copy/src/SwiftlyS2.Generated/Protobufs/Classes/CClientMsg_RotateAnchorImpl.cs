
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CClientMsg_RotateAnchorImpl : TypedProtobuf<CClientMsg_RotateAnchor>, CClientMsg_RotateAnchor
{
  public CClientMsg_RotateAnchorImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public float Angle
  { get => Accessor.GetFloat("angle"); set => Accessor.SetFloat("angle", value); }

}
