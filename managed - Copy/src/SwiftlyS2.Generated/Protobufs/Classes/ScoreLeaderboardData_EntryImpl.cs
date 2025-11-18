
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class ScoreLeaderboardData_EntryImpl : TypedProtobuf<ScoreLeaderboardData_Entry>, ScoreLeaderboardData_Entry
{
  public ScoreLeaderboardData_EntryImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Tag
  { get => Accessor.GetUInt32("tag"); set => Accessor.SetUInt32("tag", value); }


  public uint Val
  { get => Accessor.GetUInt32("val"); set => Accessor.SetUInt32("val", value); }

}
