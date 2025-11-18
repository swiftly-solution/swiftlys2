
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_ServerRankUpdate : ITypedProtobuf<CCSUsrMsg_ServerRankUpdate>, INetMessage<CCSUsrMsg_ServerRankUpdate>, IDisposable
{
  static int INetMessage<CCSUsrMsg_ServerRankUpdate>.MessageId => 352;
  
  static string INetMessage<CCSUsrMsg_ServerRankUpdate>.MessageName => "CCSUsrMsg_ServerRankUpdate";

  static CCSUsrMsg_ServerRankUpdate ITypedProtobuf<CCSUsrMsg_ServerRankUpdate>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_ServerRankUpdateImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_ServerRankUpdate_RankUpdate> RankUpdate { get; }

}
