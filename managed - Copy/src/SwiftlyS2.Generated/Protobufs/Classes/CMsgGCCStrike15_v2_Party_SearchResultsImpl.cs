
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_Party_SearchResultsImpl : TypedProtobuf<CMsgGCCStrike15_v2_Party_SearchResults>, CMsgGCCStrike15_v2_Party_SearchResults
{
  public CMsgGCCStrike15_v2_Party_SearchResultsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_Party_SearchResults_Entry> Entries
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_Party_SearchResults_Entry>(Accessor, "entries"); }

}
