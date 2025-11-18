
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface ScoreLeaderboardData : ITypedProtobuf<ScoreLeaderboardData>
{
  static ScoreLeaderboardData ITypedProtobuf<ScoreLeaderboardData>.Wrap(nint handle, bool isManuallyAllocated) => new ScoreLeaderboardDataImpl(handle, isManuallyAllocated);


  public ulong QuestId { get; set; }


  public uint Score { get; set; }


  public IProtobufRepeatedFieldSubMessageType<ScoreLeaderboardData_AccountEntries> Accountentries { get; }


  public IProtobufRepeatedFieldSubMessageType<ScoreLeaderboardData_Entry> Matchentries { get; }


  public string LeaderboardName { get; set; }

}
