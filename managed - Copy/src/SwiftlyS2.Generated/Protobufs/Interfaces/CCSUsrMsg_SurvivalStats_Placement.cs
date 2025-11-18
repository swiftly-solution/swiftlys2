
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCSUsrMsg_SurvivalStats_Placement : ITypedProtobuf<CCSUsrMsg_SurvivalStats_Placement>
{
  static CCSUsrMsg_SurvivalStats_Placement ITypedProtobuf<CCSUsrMsg_SurvivalStats_Placement>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_SurvivalStats_PlacementImpl(handle, isManuallyAllocated);


  public ulong Xuid { get; set; }


  public int Teamnumber { get; set; }


  public int Placement { get; set; }

}
