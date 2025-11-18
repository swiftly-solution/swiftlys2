
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_SurvivalStats_PlacementImpl : TypedProtobuf<CCSUsrMsg_SurvivalStats_Placement>, CCSUsrMsg_SurvivalStats_Placement
{
  public CCSUsrMsg_SurvivalStats_PlacementImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Xuid
  { get => Accessor.GetUInt64("xuid"); set => Accessor.SetUInt64("xuid", value); }


  public int Teamnumber
  { get => Accessor.GetInt32("teamnumber"); set => Accessor.SetInt32("teamnumber", value); }


  public int Placement
  { get => Accessor.GetInt32("placement"); set => Accessor.SetInt32("placement", value); }

}
