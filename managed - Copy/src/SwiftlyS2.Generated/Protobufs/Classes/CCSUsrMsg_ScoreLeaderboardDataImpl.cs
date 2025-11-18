
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_ScoreLeaderboardDataImpl : NetMessage<CCSUsrMsg_ScoreLeaderboardData>, CCSUsrMsg_ScoreLeaderboardData
{
  public CCSUsrMsg_ScoreLeaderboardDataImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public ScoreLeaderboardData Data
  { get => new ScoreLeaderboardDataImpl(NativeNetMessages.GetNestedMessage(Address, "data"), false); }

}
