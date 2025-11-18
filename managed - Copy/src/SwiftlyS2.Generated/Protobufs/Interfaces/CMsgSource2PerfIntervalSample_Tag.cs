
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSource2PerfIntervalSample_Tag : ITypedProtobuf<CMsgSource2PerfIntervalSample_Tag>
{
  static CMsgSource2PerfIntervalSample_Tag ITypedProtobuf<CMsgSource2PerfIntervalSample_Tag>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSource2PerfIntervalSample_TagImpl(handle, isManuallyAllocated);


  public string Tag { get; set; }


  public uint MaxValue { get; set; }

}
