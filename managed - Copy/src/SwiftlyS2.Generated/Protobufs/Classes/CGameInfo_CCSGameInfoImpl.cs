
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CGameInfo_CCSGameInfoImpl : TypedProtobuf<CGameInfo_CCSGameInfo>, CGameInfo_CCSGameInfo
{
  public CGameInfo_CCSGameInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldValueType<int> RoundStartTicks
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "round_start_ticks"); }

}
