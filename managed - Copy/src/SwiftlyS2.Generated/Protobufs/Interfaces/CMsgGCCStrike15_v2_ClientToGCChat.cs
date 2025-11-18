
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientToGCChat : ITypedProtobuf<CMsgGCCStrike15_v2_ClientToGCChat>
{
  static CMsgGCCStrike15_v2_ClientToGCChat ITypedProtobuf<CMsgGCCStrike15_v2_ClientToGCChat>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientToGCChatImpl(handle, isManuallyAllocated);


  public ulong MatchId { get; set; }


  public string Text { get; set; }

}
