
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_ClientDeepStats_DeepStatsRangeImpl : TypedProtobuf<CMsgGCCStrike15_ClientDeepStats_DeepStatsRange>, CMsgGCCStrike15_ClientDeepStats_DeepStatsRange
{
  public CMsgGCCStrike15_ClientDeepStats_DeepStatsRangeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Begin
  { get => Accessor.GetUInt32("begin"); set => Accessor.SetUInt32("begin", value); }


  public uint End
  { get => Accessor.GetUInt32("end"); set => Accessor.SetUInt32("end", value); }


  public bool Frozen
  { get => Accessor.GetBool("frozen"); set => Accessor.SetBool("frozen", value); }

}
