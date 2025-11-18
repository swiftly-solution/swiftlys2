
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCSUsrMsg_PlayerStatsUpdate_Stat : ITypedProtobuf<CCSUsrMsg_PlayerStatsUpdate_Stat>
{
  static CCSUsrMsg_PlayerStatsUpdate_Stat ITypedProtobuf<CCSUsrMsg_PlayerStatsUpdate_Stat>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_PlayerStatsUpdate_StatImpl(handle, isManuallyAllocated);


  public int Idx { get; set; }


  public int Delta { get; set; }

}
