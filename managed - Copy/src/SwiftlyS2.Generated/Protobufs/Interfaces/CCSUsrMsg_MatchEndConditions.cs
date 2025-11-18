
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_MatchEndConditions : ITypedProtobuf<CCSUsrMsg_MatchEndConditions>, INetMessage<CCSUsrMsg_MatchEndConditions>, IDisposable
{
  static int INetMessage<CCSUsrMsg_MatchEndConditions>.MessageId => 334;
  
  static string INetMessage<CCSUsrMsg_MatchEndConditions>.MessageName => "CCSUsrMsg_MatchEndConditions";

  static CCSUsrMsg_MatchEndConditions ITypedProtobuf<CCSUsrMsg_MatchEndConditions>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_MatchEndConditionsImpl(handle, isManuallyAllocated);


  public int Fraglimit { get; set; }


  public int MpMaxrounds { get; set; }


  public int MpWinlimit { get; set; }


  public float MpTimelimit { get; set; }

}
