
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_GCToClientChat : ITypedProtobuf<CMsgGCCStrike15_v2_GCToClientChat>
{
  static CMsgGCCStrike15_v2_GCToClientChat ITypedProtobuf<CMsgGCCStrike15_v2_GCToClientChat>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_GCToClientChatImpl(handle, isManuallyAllocated);


  public uint AccountId { get; set; }


  public string Text { get; set; }

}
