
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockResponse : ITypedProtobuf<CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockResponse>
{
  static CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockResponse ITypedProtobuf<CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockResponse>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockResponseImpl(handle, isManuallyAllocated);


  public CEconItemPreviewDataBlock Iteminfo { get; }

}
