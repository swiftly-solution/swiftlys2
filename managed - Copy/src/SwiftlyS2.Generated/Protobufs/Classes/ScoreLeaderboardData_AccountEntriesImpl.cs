
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class ScoreLeaderboardData_AccountEntriesImpl : TypedProtobuf<ScoreLeaderboardData_AccountEntries>, ScoreLeaderboardData_AccountEntries
{
  public ScoreLeaderboardData_AccountEntriesImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Accountid
  { get => Accessor.GetUInt32("accountid"); set => Accessor.SetUInt32("accountid", value); }


  public IProtobufRepeatedFieldSubMessageType<ScoreLeaderboardData_Entry> Entries
  { get => new ProtobufRepeatedFieldSubMessageType<ScoreLeaderboardData_Entry>(Accessor, "entries"); }

}
