
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class ScoreLeaderboardDataImpl : TypedProtobuf<ScoreLeaderboardData>, ScoreLeaderboardData
{
  public ScoreLeaderboardDataImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong QuestId
  { get => Accessor.GetUInt64("quest_id"); set => Accessor.SetUInt64("quest_id", value); }


  public uint Score
  { get => Accessor.GetUInt32("score"); set => Accessor.SetUInt32("score", value); }


  public IProtobufRepeatedFieldSubMessageType<ScoreLeaderboardData_AccountEntries> Accountentries
  { get => new ProtobufRepeatedFieldSubMessageType<ScoreLeaderboardData_AccountEntries>(Accessor, "accountentries"); }


  public IProtobufRepeatedFieldSubMessageType<ScoreLeaderboardData_Entry> Matchentries
  { get => new ProtobufRepeatedFieldSubMessageType<ScoreLeaderboardData_Entry>(Accessor, "matchentries"); }


  public string LeaderboardName
  { get => Accessor.GetString("leaderboard_name"); set => Accessor.SetString("leaderboard_name", value); }

}
