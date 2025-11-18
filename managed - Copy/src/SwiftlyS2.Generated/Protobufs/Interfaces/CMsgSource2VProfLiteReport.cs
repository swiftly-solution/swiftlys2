
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSource2VProfLiteReport : ITypedProtobuf<CMsgSource2VProfLiteReport>
{
  static CMsgSource2VProfLiteReport ITypedProtobuf<CMsgSource2VProfLiteReport>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSource2VProfLiteReportImpl(handle, isManuallyAllocated);


  public CMsgSource2VProfLiteReportItem Total { get; }


  public IProtobufRepeatedFieldSubMessageType<CMsgSource2VProfLiteReportItem> Items { get; }


  public uint DiscardedFrames { get; set; }

}
