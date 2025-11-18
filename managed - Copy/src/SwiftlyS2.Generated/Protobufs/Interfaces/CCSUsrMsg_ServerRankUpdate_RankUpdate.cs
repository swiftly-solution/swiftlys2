
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCSUsrMsg_ServerRankUpdate_RankUpdate : ITypedProtobuf<CCSUsrMsg_ServerRankUpdate_RankUpdate>
{
  static CCSUsrMsg_ServerRankUpdate_RankUpdate ITypedProtobuf<CCSUsrMsg_ServerRankUpdate_RankUpdate>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_ServerRankUpdate_RankUpdateImpl(handle, isManuallyAllocated);


  public int AccountId { get; set; }


  public int RankOld { get; set; }


  public int RankNew { get; set; }


  public int NumWins { get; set; }


  public float RankChange { get; set; }


  public int RankTypeId { get; set; }

}
