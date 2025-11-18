
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSource2PerfIntervalSample_TagImpl : TypedProtobuf<CMsgSource2PerfIntervalSample_Tag>, CMsgSource2PerfIntervalSample_Tag
{
  public CMsgSource2PerfIntervalSample_TagImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Tag
  { get => Accessor.GetString("tag"); set => Accessor.SetString("tag", value); }


  public uint MaxValue
  { get => Accessor.GetUInt32("max_value"); set => Accessor.SetUInt32("max_value", value); }

}
