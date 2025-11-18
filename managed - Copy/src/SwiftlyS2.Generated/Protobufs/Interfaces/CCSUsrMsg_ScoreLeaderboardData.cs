
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_ScoreLeaderboardData : ITypedProtobuf<CCSUsrMsg_ScoreLeaderboardData>, INetMessage<CCSUsrMsg_ScoreLeaderboardData>, IDisposable
{
  static int INetMessage<CCSUsrMsg_ScoreLeaderboardData>.MessageId => 367;
  
  static string INetMessage<CCSUsrMsg_ScoreLeaderboardData>.MessageName => "CCSUsrMsg_ScoreLeaderboardData";

  static CCSUsrMsg_ScoreLeaderboardData ITypedProtobuf<CCSUsrMsg_ScoreLeaderboardData>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_ScoreLeaderboardDataImpl(handle, isManuallyAllocated);


  public ScoreLeaderboardData Data { get; }

}
