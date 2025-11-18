
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCToGCBannedWordListBroadcast : ITypedProtobuf<CMsgGCToGCBannedWordListBroadcast>
{
  static CMsgGCToGCBannedWordListBroadcast ITypedProtobuf<CMsgGCToGCBannedWordListBroadcast>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCToGCBannedWordListBroadcastImpl(handle, isManuallyAllocated);


  public CMsgGCBannedWordListResponse Broadcast { get; }

}
