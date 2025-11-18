
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgQuaternionImpl : TypedProtobuf<CMsgQuaternion>, CMsgQuaternion
{
  public CMsgQuaternionImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public float X
  { get => Accessor.GetFloat("x"); set => Accessor.SetFloat("x", value); }


  public float Y
  { get => Accessor.GetFloat("y"); set => Accessor.SetFloat("y", value); }


  public float Z
  { get => Accessor.GetFloat("z"); set => Accessor.SetFloat("z", value); }


  public float W
  { get => Accessor.GetFloat("w"); set => Accessor.SetFloat("w", value); }

}
