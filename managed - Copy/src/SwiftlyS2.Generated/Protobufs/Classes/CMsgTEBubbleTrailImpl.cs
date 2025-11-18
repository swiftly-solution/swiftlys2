
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTEBubbleTrailImpl : NetMessage<CMsgTEBubbleTrail>, CMsgTEBubbleTrail
{
  public CMsgTEBubbleTrailImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public Vector Mins
  { get => Accessor.GetVector("mins"); set => Accessor.SetVector("mins", value); }


  public Vector Maxs
  { get => Accessor.GetVector("maxs"); set => Accessor.SetVector("maxs", value); }


  public float Waterz
  { get => Accessor.GetFloat("waterz"); set => Accessor.SetFloat("waterz", value); }


  public uint Count
  { get => Accessor.GetUInt32("count"); set => Accessor.SetUInt32("count", value); }


  public float Speed
  { get => Accessor.GetFloat("speed"); set => Accessor.SetFloat("speed", value); }

}
