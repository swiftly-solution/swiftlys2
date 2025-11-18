
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCToGCBannedWordListUpdated : ITypedProtobuf<CMsgGCToGCBannedWordListUpdated>
{
  static CMsgGCToGCBannedWordListUpdated ITypedProtobuf<CMsgGCToGCBannedWordListUpdated>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCToGCBannedWordListUpdatedImpl(handle, isManuallyAllocated);


  public uint GroupId { get; set; }

}
