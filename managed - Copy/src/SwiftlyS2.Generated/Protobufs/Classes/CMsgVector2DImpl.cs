
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgVector2DImpl : TypedProtobuf<CMsgVector2D>, CMsgVector2D
{
  public CMsgVector2DImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public float X
  { get => Accessor.GetFloat("x"); set => Accessor.SetFloat("x", value); }


  public float Y
  { get => Accessor.GetFloat("y"); set => Accessor.SetFloat("y", value); }

}
