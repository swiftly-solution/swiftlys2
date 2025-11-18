
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_ServerRankUpdateImpl : NetMessage<CCSUsrMsg_ServerRankUpdate>, CCSUsrMsg_ServerRankUpdate
{
  public CCSUsrMsg_ServerRankUpdateImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_ServerRankUpdate_RankUpdate> RankUpdate
  { get => new ProtobufRepeatedFieldSubMessageType<CCSUsrMsg_ServerRankUpdate_RankUpdate>(Accessor, "rank_update"); }

}
