
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCBannedWordListResponse : ITypedProtobuf<CMsgGCBannedWordListResponse>
{
  static CMsgGCBannedWordListResponse ITypedProtobuf<CMsgGCBannedWordListResponse>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCBannedWordListResponseImpl(handle, isManuallyAllocated);


  public uint BanListGroupId { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCBannedWord> WordList { get; }

}
