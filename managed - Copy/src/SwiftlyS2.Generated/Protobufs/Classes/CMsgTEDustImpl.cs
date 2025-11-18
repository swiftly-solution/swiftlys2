
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTEDustImpl : NetMessage<CMsgTEDust>, CMsgTEDust
{
  public CMsgTEDustImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public Vector Origin
  { get => Accessor.GetVector("origin"); set => Accessor.SetVector("origin", value); }


  public float Size
  { get => Accessor.GetFloat("size"); set => Accessor.SetFloat("size", value); }


  public float Speed
  { get => Accessor.GetFloat("speed"); set => Accessor.SetFloat("speed", value); }


  public Vector Direction
  { get => Accessor.GetVector("direction"); set => Accessor.SetVector("direction", value); }

}
