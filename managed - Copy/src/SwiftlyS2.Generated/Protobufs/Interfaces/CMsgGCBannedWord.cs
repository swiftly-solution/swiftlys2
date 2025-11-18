
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCBannedWord : ITypedProtobuf<CMsgGCBannedWord>
{
  static CMsgGCBannedWord ITypedProtobuf<CMsgGCBannedWord>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCBannedWordImpl(handle, isManuallyAllocated);


  public uint WordId { get; set; }


  public GC_BannedWordType WordType { get; set; }


  public string Word { get; set; }

}
