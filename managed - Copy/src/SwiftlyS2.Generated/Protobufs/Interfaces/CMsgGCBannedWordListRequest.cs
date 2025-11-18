
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCBannedWordListRequest : ITypedProtobuf<CMsgGCBannedWordListRequest>
{
  static CMsgGCBannedWordListRequest ITypedProtobuf<CMsgGCBannedWordListRequest>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCBannedWordListRequestImpl(handle, isManuallyAllocated);


  public uint BanListGroupId { get; set; }


  public uint WordId { get; set; }

}
