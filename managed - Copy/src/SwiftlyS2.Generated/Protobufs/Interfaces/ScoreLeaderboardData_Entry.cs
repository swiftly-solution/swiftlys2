
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface ScoreLeaderboardData_Entry : ITypedProtobuf<ScoreLeaderboardData_Entry>
{
  static ScoreLeaderboardData_Entry ITypedProtobuf<ScoreLeaderboardData_Entry>.Wrap(nint handle, bool isManuallyAllocated) => new ScoreLeaderboardData_EntryImpl(handle, isManuallyAllocated);


  public uint Tag { get; set; }


  public uint Val { get; set; }

}
