
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientGCRankUpdateImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientGCRankUpdate>, CMsgGCCStrike15_v2_ClientGCRankUpdate
{
  public CMsgGCCStrike15_v2_ClientGCRankUpdateImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<PlayerRankingInfo> Rankings
  { get => new ProtobufRepeatedFieldSubMessageType<PlayerRankingInfo>(Accessor, "rankings"); }

}
