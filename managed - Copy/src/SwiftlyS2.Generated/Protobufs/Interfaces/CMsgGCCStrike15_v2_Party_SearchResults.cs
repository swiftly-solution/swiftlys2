
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_Party_SearchResults : ITypedProtobuf<CMsgGCCStrike15_v2_Party_SearchResults>
{
  static CMsgGCCStrike15_v2_Party_SearchResults ITypedProtobuf<CMsgGCCStrike15_v2_Party_SearchResults>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_Party_SearchResultsImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_Party_SearchResults_Entry> Entries { get; }

}
